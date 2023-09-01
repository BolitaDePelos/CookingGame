using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{

    [SerializeField] private LayerMask InteractionLayerMask;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField][Min(1)] private float hitrange = 4;

    private RaycastHit hit;

    private void OnEnable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed += InteractionObjects;
    }


    private void OnDisable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed -= InteractionObjects;
    }


    private void Update()
    {
        if (hit.collider != null)
        {
           // hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);


        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitrange, InteractionLayerMask))
        {
            //hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

        }
    }

    private void InteractionObjects()
    {
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Fridge>())
            {

                Fridge fridgeComponent = hit.collider.GetComponent<Fridge>();

                if (fridgeComponent != null)
                {
                    fridgeComponent.Use(gameObject);
                }

            }

            if (hit.collider.GetComponent<Stove>())
            {

                Stove stoveComponent = hit.collider.GetComponent<Stove>();

                if (stoveComponent != null)
                {
                    stoveComponent.Use(gameObject);
                }

            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitrange);
    }
}


