using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector: MonoBehaviour
{
    public LayerMask detectionLayer;
    [SerializeField] private UnityEvent onCollisionEnter;
    [SerializeField] private UnityEvent onCollisionStay;
    [SerializeField] private UnityEvent onCollisionExit;
    public event System.Action<Collision> OnCollisionEntered;
    public event System.Action<Collision> OnCollisionStayed;
    public event System.Action<Collision> OnCollisionExited;


    private void OnCollisionEnter(Collision other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionEntered?.Invoke(other);
            onCollisionEnter?.Invoke();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionStayed?.Invoke(other);
            onCollisionStay?.Invoke();
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnCollisionExited?.Invoke(other);
            onCollisionExit?.Invoke();
        }
    }

}

