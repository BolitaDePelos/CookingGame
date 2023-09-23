using System.Collections;
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
        
        // TODO press twice
        if(pickable.IsPickedUp() && InputManager.GetInstance().GetLeftMousePressed())
        {
            // TODO: Execute animation of tilding.
            StartCoroutine(Rotate());
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

    private IEnumerator Rotate()
    {
        Vector3 forward = Vector3.forward;
        Vector3 vector1 = Camera.main.transform.position;
        Vector3 vector2 = transform.position;
        int timeDuration = 10;
        Vector3 dir = vector2 - vector1;

        float angle = Vector3.Angle(forward, dir);
        //float angle = Mathf.Acos(Vector2.Dot(forward.normalized, dir.normalized));
        float timeAnimation = 0;

        while (timeAnimation< timeDuration) 
        {
            timeAnimation += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,-90,-90),timeAnimation/ timeDuration);
            if (angle < 90)
            {
                print("asdasdasd");
            }
            yield return new WaitForEndOfFrame();
        
        }
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