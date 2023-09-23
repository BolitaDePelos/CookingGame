using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the plates in the counter, how they can spawn, and the sites we have.
/// </summary>
public class PlateManager : SingletonMonobehaviour<PlateManager>
{
    [Header("Plate Settings")] [SerializeField]
    private List<PlateSpawn> _plateSpawns = new();

    [SerializeField] private List<GameObject> _platePrefabs = new();
    [SerializeField] private float waitTimeUntilNextPlateSeconds = 2f;

    private float _currentWaitTimeSeconds;

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        if (_currentWaitTimeSeconds > waitTimeUntilNextPlateSeconds)
        {
            SpawnPlate();
            _currentWaitTimeSeconds = 0.0f;
        }

        _currentWaitTimeSeconds += Time.deltaTime;
    }

    /// <summary>
    /// Spawn a new plate in an empty space.
    /// </summary>
    private void SpawnPlate()
    {
        foreach (PlateSpawn plateSpawn in _plateSpawns)
        {
            if (plateSpawn.isUsed)
                continue;

            int prefabIndex = Random.Range(0, _platePrefabs.Count);
            GameObject plate = Instantiate(_platePrefabs[prefabIndex], plateSpawn.plateLocation);
            plate.transform.parent = plateSpawn.plateLocation;
            plateSpawn.isUsed = true;
            plateSpawn.currentPlate = plate;

            var plateComponent = plate.GetComponentInChildren<Plate>();
            plateComponent.OnSpawn();
            break;
        }
    }

    /// <summary>
    /// Removes a plate from the spawner cache.
    /// </summary>
    public void RemoveFromSpawner(GameObject plate)
    {
        foreach (PlateSpawn t in _plateSpawns.Where(t => t.currentPlate == plate))
        {
            t.isUsed = false;
            t.currentPlate = null;
            _currentWaitTimeSeconds = 0f;
        }
    }
}