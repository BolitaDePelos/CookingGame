using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector2D : MonoBehaviour
{
    public LayerMask detectionLayer;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;
    public event System.Action<Transform> OnTriggerEntered;
    public event System.Action<Transform> OnTriggerExited;
    public event System.Action<Transform> OnTriggerStayed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, collision.gameObject))
        {
            OnTriggerEntered?.Invoke(collision.transform);
            onTriggerEnter?.Invoke();
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnTriggerStayed?.Invoke(other.transform);
            onTriggerStay?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnTriggerExited?.Invoke(other.transform);
            onTriggerExit?.Invoke();
        }
    }
}

