using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the plates in the counter, how they can spawn, and the sites we have.
/// </summary>
public class PlateManager : MonoBehaviour
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
        (PlateSpawn plateSpawn, int index) = _plateSpawns.GetNextEmpty();
        if (plateSpawn == null)
            return;

        int prefabIndex = Random.Range(0, _platePrefabs.Count);
        GameObject plate = Instantiate(_platePrefabs[prefabIndex], plateSpawn.plateLocation);
        plate.transform.parent = transform;

        plateSpawn.isUsed = true;
        plateSpawn.currentPlate = plate;
        _plateSpawns[index] = plateSpawn;

        var plateComponent = plate.GetComponentInChildren<Plate>();
        plateComponent.OnSpawn();
    }
}