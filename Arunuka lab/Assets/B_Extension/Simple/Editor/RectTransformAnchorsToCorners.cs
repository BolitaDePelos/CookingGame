using UnityEditor;
using UnityEngine;

public class RectTransformAnchorsToCorners
{
    [MenuItem("B_Extensions/UI/Anchors To Corners %F2")]
    static void SetEasyAnchors()
    {
        var objs = Selection.gameObjects;

        foreach (var o in objs)
        {
            if (o != null && o.GetComponent<RectTransform>() != null)
            {
                var r = o.GetComponent<RectTransform>();
                var p = o.transform.parent.GetComponent<RectTransform>();

                var offsetMin = r.offsetMin;
                var offsetMax = r.offsetMax;
                var _anchorMin = r.anchorMin;
                var _anchorMax = r.anchorMax;

                var parent_width = p.rect.width;
                var parent_height = p.rect.height;

                var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                    _anchorMin.y + (offsetMin.y / parent_height));
                var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                    _anchorMax.y + (offsetMax.y / parent_height));

                r.anchorMin = anchorMin;
                r.anchorMax = anchorMax;

                r.offsetMin = new Vector2(0, 0);
                r.offsetMax = new Vector2(0, 0);
                r.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    }

    [MenuItem("B_Extensions/UI/Anchors To Center")]
    static void SetAnchorsToCenter()
    {
        var objs = Selection.gameObjects;

        foreach (var o in objs)
        {
            if (o != null && o.GetComponent<RectTransform>() != null)
            {
                var r = o.GetComponent<RectTransform>();
                var p = o.transform.parent.GetComponent<RectTransform>();

                // Guardamos la posición actual antes de modificar los anchors
                var currentPosition = r.localPosition.x;
                var currentPositionY = r.localPosition.y;

                Debug.Log((currentPosition+ (p.rect.width/2))/p.rect.width);
                Debug.Log((currentPositionY + (p.rect.height / 2)) / p.rect.height);

                var targetAnchorPos = new Vector2(
                    (currentPosition + (p.rect.width / 2)) / p.rect.width, 
                    (currentPositionY + (p.rect.height / 2)) / p.rect.height);

                //p.rect.width;

                // Calculamos el centro normalizado del RectTransform respecto al padre
                //float centerX = (r.rect.width * 0.5f) / p.rect.width;
                //float centerY = (r.rect.height * 0.5f) / p.rect.height;
                //Debug.Log($"{centerX} __ {centerY}");

                //// Establecemos ambos anchors en el centro de la imagen
                //r.anchorMin = new Vector2(0.5f - centerX, 0.5f - centerY);
                //r.anchorMax = new Vector2(0.5f + centerX, 0.5f + centerY);

                //// Restauramos la posición y ajustamos los offsets
                //r.anchoredPosition = currentPosition;
                r.anchorMin = new Vector2(targetAnchorPos.x,targetAnchorPos.x);
                r.anchorMax = new Vector2(targetAnchorPos.y, targetAnchorPos.y); 
                r.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    }
}
