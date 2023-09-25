using Unity.VisualScripting;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] [Min(1)] private float hitRange = 3;

    [SerializeField] private Transform pickUpParent;

    [SerializeField] private GameObject inHandItem;

    [SerializeField] private AudioSource pickUpSource;

    [SerializeField] private Player player;

    [SerializeField] private Transform cutPositionPlayer;

    private RaycastHit _hit;

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
        if (inHandItem == null)
            HoverCursor.Instance.OnExitHover();

        // Just used to keep reference of the hit.
        //
        if (Physics.Raycast(
                playerCameraTransform.position,
                playerCameraTransform.forward,
                out _hit,
                hitRange,
                pickableLayerMask))
        {
            if (_hit.collider != null)
                HoverCursor.Instance.OnHover();
        }

        if (inHandItem != null)
            HoverCursor.Instance.OnGrab();

        
    }

    private void UseItem()
    {
        if (inHandItem == null)
            return;

        if (inHandItem.TryGetComponent(out IUsable usable))
            usable.Use(gameObject);
    }

    private void PickUp()
    {
        if (_hit.collider == null || inHandItem != null)
            return;

        if (!_hit.collider.TryGetComponent(out IPickable pickableItem))
            return;

        if (!pickableItem.IsPickable())
            return;

        pickUpSource.Play();
        inHandItem = pickableItem.PickUp(pickUpParent.gameObject);

        HoverCursor.Instance.OnGrab();
        if (_hit.collider.GetComponent<Knife>())
        {
            Debug.Log("It's a Knife!");
            player.GetComponentInParent<Animator>().SetTrigger("Knife");
            PlayerSetCutPosition();
        }
    }

    private void DropItem()
    {
        // Resumes player movement.
        //
        player.SetCanMove(true);

        if (inHandItem == null)
            return;

        //Reset Animation.
        //
        player.GetComponentInParent<Animator>().Play("Default");

        // Execute drop behaviour for item.
        //
        if (inHandItem.TryGetComponent(out IPickable pickableItem))
            pickableItem.Drop();

        inHandItem = null;
        HoverCursor.Instance.OnExitHover();
    }

    private void PlayerSetCutPosition()
    {
        // Stops temporally player movement.
        //
        player.SetCanMove(false);

        // Set the cutting position, if we have one.
        //
        if (cutPositionPlayer != null)
            player.transform.position = cutPositionPlayer.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange);
    }
}