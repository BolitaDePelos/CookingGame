using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the plates in the counter, how they can spawn, and the sites we have.
/// </summary>
public class PlateManager : SingletonMonobehaviour<PlateManager>
{
    [Header("Plate Settings")] [SerializeField]
    private List<PlateSpawn> plateSpawns = new();

    /// <summary>
    /// Spawn a new plate in an empty space.
    /// </summary>
    public void SpawnPlate(GameObject platePrefab, Recipe recipe)
    {
        foreach (PlateSpawn plateSpawn in plateSpawns)
        {
            if (plateSpawn.isUsed)
                continue;

            GameObject plate = Instantiate(platePrefab, plateSpawn.plateLocation);
            plate.transform.parent = plateSpawn.plateLocation;
            plateSpawn.isUsed = true;
            plateSpawn.currentPlate = plate;

            var plateComponent = plate.GetComponentInChildren<Plate>();
            plateComponent.OnSpawn(recipe);

            break;
        }
    }

    /// <summary>
    /// Removes a plate from the spawner cache.
    /// </summary>
    public void RemoveFromSpawner(GameObject plate)
    {
        foreach (PlateSpawn plateSpawn in plateSpawns.Where(p => p.currentPlate == plate))
        {
            plateSpawn.isUsed = false;
            plateSpawn.currentPlate = null;
        }
    }
}