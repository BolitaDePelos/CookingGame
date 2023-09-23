using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] public bool isUsed;

    /// <summary>
    /// The plate we have in position.
    /// </summary>
    [SerializeField] public GameObject currentPlate;
}

/// <summary>
/// Extensions for <see cref="PlateSpawn"/>.
/// </summary>
public static class PlateSpawnExtensions
{
    /// <summary>
    /// Gets the next empty <see cref="PlateSpawn"/> in the list.
    /// If there is not empty spawn, then it returns null.
    /// </summary>
    public static (PlateSpawn, int index) GetNextEmpty(this IEnumerable<PlateSpawn> plateSpawns)
    {
        return plateSpawns
            .Where(plateSpawn => !plateSpawn.isUsed)
            .Select((plateSpawn, index) => (plateSpawn, index))
            .FirstOrDefault();
    }
}