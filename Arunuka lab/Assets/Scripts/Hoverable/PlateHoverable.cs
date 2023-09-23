using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Have hoverable properties.
/// </summary>
public class PlateHoverable : HoverableBase
{
    [SerializeField] private UnityEvent onButtonClickEvent;

    /// <summary>
    /// Called every game frame.
    /// </summary>
    public void Update()
    {
        if (!IsHoverOn())
            return;

        if (!Input.GetKeyDown(KeyCode.G))
            return;

        onButtonClickEvent?.Invoke();
    }
}