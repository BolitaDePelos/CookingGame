using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{

    [SerializeField] private LayerMask pickableLayerMask;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] [Min(1)] private float hitrange = 3;

    [SerializeField] Transform pickUpParent;

    [SerializeField] private GameObject inHandItem;

    private RaycastHit hit;

    [SerializeField] private AudioSource pickUpSource;

    private void OnEnable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed += PickUp;
        GameEventsManager.instance.InputEvents.OnDropPressed += DropItem;
        GameEventsManager.instance.InputEvents.OnUsePressed += UseItem;


    }


    private void OnDisable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed -= PickUp;
        GameEventsManager.instance.InputEvents.OnDropPressed -= DropItem;
        GameEventsManager.instance.InputEvents.OnUsePressed -= UseItem;


    }


    private void Update()
    {
        if (hit.collider != null)
        {
           // hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            

        }

        if(inHandItem != null)
        {
            return;
        }


        if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitrange, pickableLayerMask))
        {
            //hit.collider.GetComponent <Highlight>()?.ToggleHighlight(true);
            
        }
    }


    private void UseItem()
    {

        if(inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if(usable != null)
            {
                usable.Use(this.gameObject);
            }
        }

    }

    private void PickUp()
    {
        if(hit.collider != null && inHandItem ==null)
        {
            
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if(pickableItem != null)
            {
                pickUpSource.Play();
                inHandItem = pickableItem.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.keepWorldPosition);
            }
            

            if (hit.collider.GetComponent<Knife>())
            {
                Debug.Log("Its Knife!");
                hit.collider.GetComponentInParent<Animator>().SetTrigger("Knife");
            }
        }
    }


    private void DropItem()
    {

        if (inHandItem != null)
        {
            //Reset Animation
            Animator animator = hit.collider.GetComponentInParent<Animator>();
            animator.Play("Default");

            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            


            if (rb != null)
            {
                rb.isKinematic = false;
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitrange);
    }
}
