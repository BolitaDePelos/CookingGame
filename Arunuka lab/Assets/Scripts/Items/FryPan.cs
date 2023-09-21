using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryPan : SingletonMonobehaviour<FryPan>
{
    //[Header("Pouring Properties")]
    //[SerializeField] private float liquidFillDurationSeconds = 10;
    //[SerializeField] private MeshRenderer liquidRenderer;

    [Header("Extra properties")]
    [SerializeField] private List<GameObject> foodInsidePot = new();
    [SerializeField] private bool stoveIsActive = false;
    [SerializeField] private ParticleSystem smokeParticles;

    private bool isBeingPoured = false;
    private float currentPouringDuration = 0.0f;

    private const float MIN_HEIGHT = 0.42F;
    private const float MAX_HEIGHT = 0.52F;

    private void Update()
    {
        if(stoveIsActive && !smokeParticles.isPlaying)
            smokeParticles.Play();
        
        if(!stoveIsActive && smokeParticles.isPlaying)
            smokeParticles.Stop();

        //UpdateLiquid();
    }

    /// <summary>
    /// Checks what objects are now inside to disable the pickability behavior.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IPickable pickable))
            return;

        if (foodInsidePot.Contains(other.gameObject))
            return;

        if (pickable.IsPickedUp())
            return;

        pickable.SetIsPickable(false);
        foodInsidePot.Add(other.gameObject);

        if (!other.TryGetComponent(out Food food))
            return;

        food.SetIsBeingCooked(stoveIsActive);
        food.SetFoodLocation(FoodLocation.Pan);
    }

    /// <summary>
    /// Sets if the stove is active or not.
    /// </summary>
    public void SetStoveActive(bool isActive)
    {
        stoveIsActive = isActive;
        foodInsidePot.ForEach(foodObject =>
        {
            if (!foodObject.TryGetComponent(out Food food))
                return;

            food.SetIsBeingCooked(isActive);
        });
    }

    /// <summary>
    /// Sets if liquid is being poured in the pot or not.
    /// </summary>
    public void SetIsBeingPoured(bool isBeingPoured) => this.isBeingPoured = isBeingPoured;

    //private void UpdateLiquid()
    //{
    //    if (!isBeingPoured)
    //        return;

    //    if (liquidRenderer == null)
    //        return;

    //    if (currentPouringDuration > liquidFillDurationSeconds)
    //        return;

    //    float percent = currentPouringDuration / liquidFillDurationSeconds;
    //    float fill = MIN_HEIGHT + ((MAX_HEIGHT - MIN_HEIGHT) * percent);
    //    liquidRenderer.material.SetFloat("_Fill", fill);

    //    currentPouringDuration += Time.deltaTime;
    //}
}
