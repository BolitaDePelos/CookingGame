using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <inheritdoc />
    public GameObject PickUp(GameObject picker)
    {
        gameObject.transform.SetParent(picker.transform, KeepWorldPosition);

        if (rb != null)
            rb.isKinematic = true;

        return gameObject;
    }

    /// <inheritdoc />
    public void Drop()
    {
        gameObject.transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;
    }
}