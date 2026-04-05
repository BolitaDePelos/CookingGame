using UnityEngine;
using UnityEngine.EventSystems;

public class SliderCheck : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public event System.Action<PointerEventData> OnPoint;
    public void OnDrag(PointerEventData eventData)
    {
        OnPoint?.Invoke(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPoint?.Invoke(eventData);
    }
}
