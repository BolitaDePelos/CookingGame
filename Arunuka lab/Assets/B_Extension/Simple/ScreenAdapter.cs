using System.Collections;
using UnityEngine;

public class ScreenAdapter : MonoBehaviour
{
    Canvas canvasParent;
    private Vector3 proportionalityFactor;
    public Vector3 scaleCanvas { get; private set; }
    public Vector3 scaleTransform { get; private set; }


    private void Awake() 
    {
        scaleTransform = transform.localScale;
    }

    IEnumerator Start()
    {
        canvasParent = GetComponentInParent<Canvas>();
        yield return new WaitForEndOfFrame();
        var rectscale = (RectTransform)canvasParent.transform;
        scaleCanvas = rectscale.localScale;
        proportionalityFactor = new Vector3(scaleCanvas.x/scaleTransform.x, scaleCanvas.y / scaleTransform.y, scaleCanvas.z / scaleTransform.z);
    }

    private void LateUpdate()
    {
        transform.localScale = UpdateScale(canvasParent.transform.localScale);
    }

    private Vector3 UpdateScale(Vector3 scaleNew)
    {
        var par = 
        new Vector3(
            scaleNew.x / proportionalityFactor.x,
            scaleNew.y / proportionalityFactor.y,
            scaleNew.z / proportionalityFactor.z);

        if (IsVectorInfinity(par))
            return Vector3.one;

        return par;
    }

    bool IsVectorInfinity(Vector3 vector)
    {
        return float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z);
    }
}
