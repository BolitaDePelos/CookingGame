using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    public bool keepWorldPosition { get; private set; }

    private Rigidbody rb;
    public bool tutorialMode;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start(){
        if (tutorialMode)
            enabled = false;


    }
    public GameObject PickUp()
    {

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        return this.gameObject;

    }
}
