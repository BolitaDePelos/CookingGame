using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    /// <inheritdoc />
    public bool KeepWorldPosition { get; private set; }

    public bool tutorialMode;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        enabled = !tutorialMode;
    }

    /// <inheritdoc />
    public GameObject PickUp(GameObject picker)
    {
        gameObject.transform.SetParent(picker.transform, KeepWorldPosition);

        if (rb != null)
            rb.isKinematic = true;

        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
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