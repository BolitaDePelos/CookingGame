using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; set; }

    private Rigidbody rb;
    private bool isPickable = true;
    private bool isPickedUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <inheritdoc />
    public GameObject PickUp(GameObject picker)
    {
        isPickedUp = true;
        gameObject.transform.SetParent(picker.transform, KeepWorldPosition);

        if (rb != null)
            rb.isKinematic = true;

        return gameObject;
    }

    /// <inheritdoc />
    public void Drop()
    {
        isPickedUp = false;
        gameObject.transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;
    }

    /// <inheritdoc />
    public void SetIsPickable(bool isPickable) => this.isPickable = isPickable;

    /// <inheritdoc />
    public bool IsPickable() => isPickable;

    /// <inheritdoc />
    public bool IsPickedUp() => isPickedUp;
}