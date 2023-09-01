using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{


    [SerializeField] private LayerMask draggableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] [Min(1)] private float hitrange = 3;
    private GameObject DragObject;
    private RaycastHit hit;
    private float initialDistance;
    private Vector3 initialPosition; // Si se requiere que el objecto vuelva a la posicion

    private void OnEnable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed += DragItem;
        GameEventsManager.instance.InputEvents.OnUsePressed += UseItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.InputEvents.OnInteractionPressed -= DragItem;
        GameEventsManager.instance.InputEvents.OnUsePressed -= UseItem;
    }

    private void Update()
    {
        if (hit.collider != null)
        {
            if (DragObject == null || hit.collider.gameObject != DragObject)
            {
             //   hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            }
        }

        if (DragObject != null)
        {
            Vector3 newPosition = playerCameraTransform.position + playerCameraTransform.forward * initialDistance;
            DragObject.transform.position = newPosition;

            if (Input.GetMouseButtonUp(0)) /// Toca cambiar esto pero nose como se hace usando el nuevo input manager Del scrip InputManager (Buscar como se hace lo del mouseDown and mouseUp)
            {
                DropItem();
            }
        }
        else
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitrange, draggableLayerMask))
            {
               // hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

                if (Input.GetMouseButtonDown(0))  /// Toca cambiar esto pero nose como se hace usando el nuevo input manager Del scrip InputManager (Buscar como se hace lo del mouseDown and mouseUp)
                {
                    DragItem();
                }
            }
        }
    }

    private void UseItem()
    {
        if (DragObject != null)
        {
            IUsable usable = DragObject.GetComponent<IUsable>();
            if (usable != null)
            {
                usable.Use(this.gameObject);
            }
        }
    }

    private void DragItem()
    {
        if (hit.collider != null)
        {
            DragObject = hit.collider.gameObject;
            initialDistance = Vector3.Distance(playerCameraTransform.position, DragObject.transform.position);
            initialPosition = DragObject.transform.position;

            // Desactiva la gravedad, cinemática y colisiones físicas mientras se arrastra el objeto.
            Rigidbody rb = DragObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }
        }
    }

    private void DropItem()
    {
        if (DragObject != null)
        {
            // Restablece la gravedad, cinemática y colisiones físicas del objeto.
            Rigidbody rb = DragObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            }

            // Restablece la posición del objeto a la posición inicial si es necesario.
            //DragObject.transform.position = initialPosition;

            DragObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitrange);
    }
}
