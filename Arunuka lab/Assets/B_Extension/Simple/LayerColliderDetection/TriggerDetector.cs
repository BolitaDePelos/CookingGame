using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerDetector: MonoBehaviour
{
    public LayerMask detectionLayer;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;
    public event System.Action<Transform> OnTriggerEntered;
    public event System.Action<Transform> OnTriggerExited;
    public event System.Action<Transform> OnTriggerStayed;

    private void OnTriggerEnter(Collider other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnTriggerEntered?.Invoke(other.transform);
            onTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnTriggerStayed?.Invoke(other.transform);
            onTriggerStay?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerDetection.DetectContainedLayers(detectionLayer, other.gameObject))
        {
            OnTriggerExited?.Invoke(other.transform);
            onTriggerExit?.Invoke();
        }
    }
}