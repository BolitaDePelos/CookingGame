using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the state and ingredients in the fry pan object.
/// </summary>
public class FryPan : SingletonMonobehaviour<FryPan>
{
    [SerializeField] private bool stoveIsActive;
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private List<GameObject> foodInsidePot = new();
    AudioManager audioManager;

    private void Start() => audioManager = AudioManager.Instance;


    /// <summary>
    /// Updates each game frame.
    /// </summary>
    private void Update()
    {
        switch (stoveIsActive)
        {
            case true when !smokeParticles.isPlaying:
                smokeParticles.Play();
                audioManager.PlaySoundFire();
                break;
            case false when smokeParticles.isPlaying:
                smokeParticles.Stop();
                break;
        }
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
}