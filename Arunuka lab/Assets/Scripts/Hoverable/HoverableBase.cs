using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Object than inherits it will have hover-on logic, and actions.
/// </summary>
public abstract class HoverableBase : MonoBehaviour
{
    [SerializeField] private UnityEvent onHoverEnter;
    [SerializeField] private UnityEvent onHoverExit;

    private bool _isHoverable = true;
    private bool _isHoverOn;
    /// <summary>
    /// Executed when the object is hover on.
    /// </summary>
    public virtual void OnHoverEnter()
    {
        _isHoverOn = true;
        onHoverEnter?.Invoke();
        AudioManager.Instance.PlayHoverSound();
    }

    /// <summary>
    /// Executed when the object is hover on.
    /// </summary>
    public virtual void OnHoverExit()
    {
        _isHoverOn = false;
        onHoverExit?.Invoke();
    }

    /// <summary>
    /// Sets if the object can be hoverable or not.
    /// </summary>
    public void SetHoverable(bool isHoverable)
    {
        _isHoverable = isHoverable;
    }

    /// <summary>
    /// If it's hoverable or not.
    /// </summary>
    public bool IsHoverable()
    {
        return _isHoverable;
    }

    /// <summary>
    /// If it's being hovered right now.
    /// </summary>
    public bool IsHoverOn()
    {
        return _isHoverOn;
    }
}