using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField] public bool KeepWorldPosition { get; set; }

    private Rigidbody _rigidbody;
    private bool _isPickable = true;
    private bool _isPickedUp;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <inheritdoc />
    public GameObject PickUp(GameObject picker)
    {
        _isPickedUp = true;
        gameObject.transform.SetParent(picker.transform, KeepWorldPosition);

        if (_rigidbody != null)
            _rigidbody.isKinematic = true;

        return gameObject;
    }

    /// <inheritdoc />
    public void Drop()
    {
        _isPickedUp = false;
        gameObject.transform.SetParent(null);

        if (_rigidbody != null)
            _rigidbody.isKinematic = false;
    }

    /// <inheritdoc />
    public void SetIsPickable(bool isPickable)
    {
        _isPickable = isPickable;
    }

    /// <inheritdoc />
    public bool IsPickable()
    {
        return _isPickable;
    }

    /// <inheritdoc />
    public bool IsPickedUp()
    {
        return _isPickedUp;
    }
}