using System;
using UnityEngine;

public class PositionRectFitter : MonoBehaviour
{
    [SerializeField] Vector3 positionHorizontal;
    [SerializeField] Vector3 positionVertical;

    RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    private void Update()
    {
        if (Screen.width > Screen.height)
            ApplyHorizontalPosition();
        else
            ApplyVerticalPosition();
    }

    private void ApplyVerticalPosition()
    {
        _rectTransform.anchoredPosition = positionVertical;
    }

    private void ApplyHorizontalPosition()
    {
        _rectTransform.anchoredPosition = positionHorizontal;
    }
}
