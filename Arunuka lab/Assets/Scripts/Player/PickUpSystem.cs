using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField][Min(1)] private float hitrange = 3;

    [SerializeField] private Transform pickUpParent;

    [SerializeField] private GameObject inHandItem;

    [SerializeField] private AudioSource pickUpSource;

    [SerializeField] private Player player;

    [SerializeField] private Transform cutPositionPlayer;

    private RaycastHit hit;

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

        if (inHandItem != null)
            return;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitrange, pickableLayerMask))
        {
            //hit.collider.GetComponent <Highlight>()?.ToggleHighlight(true);
        }
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
        if (hit.collider == null || inHandItem != null)
            return;

        if (!hit.collider.TryGetComponent(out IPickable pickableItem))
            return;

        if (!pickableItem.IsPickable())
            return;

        pickUpSource.Play();
        inHandItem = pickableItem.PickUp(pickUpParent.gameObject);

        if (hit.collider.GetComponent<Knife>())
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
    }

    public void PlayerSetCutPosition()
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
        Gizmos.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitrange);
    }
}