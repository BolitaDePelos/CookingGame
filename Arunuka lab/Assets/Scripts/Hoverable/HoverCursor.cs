using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverCursor : Singleton<HoverCursor>
{
    [SerializeField] private Image point;

    [SerializeField] Sprite textureHover;
    [SerializeField] Sprite textureNormal;
    [SerializeField] Sprite textureGrab;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        if (point == null)
            Cursor.visible = true;
        else
            OnExitHover();
    }

    public void OnGrab() => point.sprite = textureGrab;

    public void OnHover() => point.sprite = textureHover;

    public void OnExitHover() => point.sprite = textureNormal;
}
