using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the object than can be used by the player.
/// </summary>
public interface IUsable
{
    /// <summary>
    /// Commands to execute when the player use the object.
    /// </summary>
    UnityEvent OnUse { get; }

    /// <summary>
    /// Executes <see cref="OnUse"/>, and extra behaviour.
    /// </summary>
    void Use(GameObject actor);
}