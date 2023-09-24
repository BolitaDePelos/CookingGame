using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mechanics of a pot.
/// </summary>
public class Pot : SingletonMonobehaviour<Pot>
{
    [Header("Pouring Properties")] [SerializeField]
    private float liquidFillDurationSeconds = 10;

    [SerializeField] private MeshRenderer liquidRenderer;

    [Header("Extra properties")] [SerializeField]
    private List<GameObject> foodInsidePot = new();

    [SerializeField] private bool stoveIsActive;
    [SerializeField] private ParticleSystem smokeParticles;

    private bool _isBeingPoured;
    private float _currentPouringDuration;
    private static readonly int FillPropertyId = Shader.PropertyToID("_Fill");

    private const float MinHeight = 0.42F;
    private const float MaxHeight = 0.52F;
    AudioManager audioManager;

    private void Start() => audioManager = AudioManager.Instance;

    private void Update()
    {
        switch (stoveIsActive)
        {
            case true when !smokeParticles.isPlaying:
                smokeParticles.Play();
                break;
            case false when smokeParticles.isPlaying:
                smokeParticles.Stop();
                break;
        }

        UpdateLiquid();
    }

    /// <summary>
    /// Checks what food is now inside of the fry pan.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IPickable pickable))
            return;

        if (foodInsidePot.Contains(other.gameObject))
            return;

        if (pickable.IsPickedUp())
            return;

        foodInsidePot.Add(other.gameObject);

        if (!other.TryGetComponent(out Food food))
            return;

        audioManager.PlaySoundFire();
        food.SetIsBeingCooked(stoveIsActive);
        food.SetFoodLocation(FoodLocation.Pan);
    }

    /// <summary>
    /// Checks if a food is now outside of the fry pan.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IPickable _))
            return;

        if (!foodInsidePot.Contains(other.gameObject))
            return;

        foodInsidePot.Remove(other.gameObject);
        if (!other.TryGetComponent(out Food food))
            return;

        food.SetIsBeingCooked(false);
        food.SetFoodLocation(FoodLocation.Table);
    }

    /// <summary>
    /// Sets if the stove is active or not.
    /// </summary>
    public void SetStoveActive(bool isActive)
    {
        stoveIsActive = isActive;
        foodInsidePot.ForEach(
            foodObject =>
            {
                if (!foodObject.TryGetComponent(out Food food))
                    return;

                food.SetIsBeingCooked(isActive);
            });
    }

    /// <summary>
    /// Sets if liquid is being poured in the pot or not.
    /// </summary>
    public void SetIsBeingPoured(bool isBeingPoured)
    {
        _isBeingPoured = isBeingPoured;
    }

    /// <summary>
    /// Updates the liquid mesh renderer if it's being poured.
    /// </summary>
    private void UpdateLiquid()
    {
        if (!_isBeingPoured)
            return;

        if (liquidRenderer is null)
            return;

        if (_currentPouringDuration > liquidFillDurationSeconds)
            return;

        float percent = _currentPouringDuration / liquidFillDurationSeconds;
        float fill = MinHeight + (MaxHeight - MinHeight) * percent;
        liquidRenderer.material.SetFloat(FillPropertyId, fill);

        _currentPouringDuration += Time.deltaTime;
    }
}