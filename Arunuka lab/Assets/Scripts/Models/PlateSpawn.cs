using System;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Contains all the data for a plate zone.
/// </summary>
[Serializable]
public class PlateSpawn
{
    /// <summary>
    /// Where the plane has to be located on spawn.
    /// </summary>
    [SerializeField] public Transform plateLocation;

    /// <summary>
    /// If the spawn is used or not.
    /// </summary>
    [ReadOnly] [SerializeField] public bool isUsed;

    /// <summary>
    /// The plate we have in position.
    /// </summary>
    [ReadOnly] [SerializeField] public GameObject currentPlate;
}