using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Have hoverable properties.
/// </summary>
public class PlateHoverable : HoverableBase
{
    [SerializeField] private Plate plate;
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

        if (plate.IsEmpty())
        {
            // TODO: Show something to indicate the plate is empty.
            return;
        }

        onButtonClickEvent?.Invoke();
    }
}