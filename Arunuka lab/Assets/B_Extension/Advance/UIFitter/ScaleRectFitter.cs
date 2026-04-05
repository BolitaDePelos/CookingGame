using UnityEngine;

public class ScaleRectFitter : MonoBehaviour
{
    [SerializeField] Vector3 scaleHorizontal;
    [SerializeField] Vector3 scaleVertical;

    RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    private void Update()
    {
        if (Screen.width > Screen.height)
            ApplyHorizontalScale();
        else
            ApplyVerticalScale();
    }

    private void ApplyVerticalScale() => _rectTransform.localScale = scaleVertical;

    private void ApplyHorizontalScale() => _rectTransform.localScale = scaleHorizontal;
}
