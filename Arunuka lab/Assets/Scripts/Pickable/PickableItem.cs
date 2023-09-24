using System.Security.Cryptography;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    /// <inheritdoc />
    public bool KeepWorldPosition { get; private set; }
    public bool tutorialMode;
    private Rigidbody rb;
    private bool isPickable = true;
    private bool isPickedUp = false;

    private void Start() 
    {
        //AudioManager.Instance;
    
    }


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
        isPickedUp = true;
        gameObject.transform.SetParent(picker.transform, KeepWorldPosition);

        if (rb != null)
            rb.isKinematic = true;

        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        
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