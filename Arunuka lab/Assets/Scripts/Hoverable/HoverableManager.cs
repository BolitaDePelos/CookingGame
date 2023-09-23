using UnityEngine;

/// <summary>
/// Enables or disables hoverable objects based on the POV of the player.
/// </summary>
public class HoverableManager : MonoBehaviour
{
    [SerializeField] private LayerMask hoverableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] [Min(1)] private float hitRange = 3;
    [SerializeField] private GameObject currentHoverObject;

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        if (Physics.Raycast(
                playerCameraTransform.position,
                playerCameraTransform.forward,
                out RaycastHit hit,
                hitRange,
                hoverableLayerMask))
        {
            if (hit.collider == null || currentHoverObject != null)
                return;

            if (!hit.collider.TryGetComponent(out HoverableBase hoverable))
                return;

            hoverable.OnHoverEnter();
            currentHoverObject = hit.collider.gameObject;
        }
        else if (currentHoverObject != null)
        {
            var hoverable = currentHoverObject.GetComponent<HoverableBase>();
            hoverable.OnHoverExit();
            currentHoverObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange);
    }
}