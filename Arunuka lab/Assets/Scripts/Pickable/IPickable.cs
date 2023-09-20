using UnityEngine;

/// <summary>
/// Pickable object.
/// </summary>
public interface IPickable
{
    /// <summary>
    /// If the object keeps the world position when it's picked up.
    /// </summary>
    bool KeepWorldPosition { get; }

    /// <summary>
    /// Logic executed when the object in picked up.
    /// </summary>
    GameObject PickUp(GameObject picker);

    /// <summary>
    /// Logic executed when the object in dropped.
    /// </summary>
    void Drop();
}