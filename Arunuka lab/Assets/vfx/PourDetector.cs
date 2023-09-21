using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    private IPickable pickable;

    private void Awake()
    {
        pickable = GetComponent<IPickable>();
    }

    private void Update()
    {
        
        if(pickable.IsPickedUp() && InputManager.GetInstance().GetLeftMousePressed())
        {
            // TODO: Execute animation of tilding.
        }

        bool pourCheck = CalculatePourAngle() < pourThreshold;
        if (isPouring == pourCheck)
            return;

        isPouring = pourCheck;
        if (isPouring)
            StartPour();
        else
            EndPour();
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        return transform.forward.z * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}