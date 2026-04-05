using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector2D: MonoBehaviour
{
    public LayerMask detectionLayer;
    [SerializeField] private UnityEvent onCollisionEnter;
    [SerializeField] private UnityEvent onCollisionStay;
    [SerializeField] private UnityEvent onCollisionExit;
    public event System.Action<Collision2D> OnCollisionEntered;
    public event System.Action<Collision2D> OnCollisionStayed;
    public event System.Action<Collision2D> OnCollisionExited;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionEntered?.Invoke(other);
            onCollisionEnter?.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionStayed?.Invoke(other);
            onCollisionStay?.Invoke();
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionExited?.Invoke(other);
            onCollisionExit?.Invoke();
        }
    }

}

