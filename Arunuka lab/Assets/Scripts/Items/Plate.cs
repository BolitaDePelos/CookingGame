using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Manages the ingredients placed in the plate.
/// </summary>
public class Plate : MonoBehaviour
{
    [Header("Food")] [SerializeField] private List<GameObject> foodOnPlate = new();
    [SerializeField] private float destroyAfterSeconds = 1.0f;

    private bool _mustDestroy;
    private float _currentDestroySeconds;

    private void Update()
    {
        if (!_mustDestroy)
            return;

        _currentDestroySeconds += Time.deltaTime;
        if (!(_currentDestroySeconds > destroyAfterSeconds))
            return;

        // Remove plate container
        //
        Transform parent = transform.parent;
        PlateManager.Instance.RemoveFromSpawner(parent.gameObject);
        DestroyImmediate(parent.gameObject);
    }

    /// <summary>
    /// Checks what food is now inside of the plate.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IPickable pickable))
            return;

        if (foodOnPlate.Contains(other.gameObject))
            return;

        if (pickable.IsPickedUp())
            return;

        foodOnPlate.Add(other.gameObject);

        if (!other.TryGetComponent(out Food food))
            return;

        food.SetFoodLocation(FoodLocation.Plate);
    }

    /// <summary>
    /// Checks if a food is now outside of the plate.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IPickable _))
            return;

        if (!foodOnPlate.Contains(other.gameObject))
            return;

        foodOnPlate.Remove(other.gameObject);
        if (!other.TryGetComponent(out Food food))
            return;

        food.SetFoodLocation(FoodLocation.Table);
    }

    public void FinishPlate()
    {
        StringBuilder builder = new();

        foodOnPlate.ForEach(
            foodObject =>
            {
                var pickable = foodObject.GetComponent<IPickable>();
                pickable.SetIsPickable(false);

                if (!foodObject.TryGetComponent(out Food food))
                    return;

                foodObject.transform.parent = transform;
                string ingredientName = food.IngredientName;
                FoodCookState cookState = food.GetFoodState();
                float cookDuration = food.GetCookedTimeSeconds();

                string description
                    = $"{ingredientName} was cooked for {cookDuration:G}s, "
                      + $"so it's {Enum.GetName(typeof(FoodCookState), cookState)}.";

                builder.AppendLine(description);
            });

        _mustDestroy = true;
        Debug.Log(builder.ToString());
    }

    /// <summary>
    /// Executed when the object in spawned.
    /// </summary>
    public void OnSpawn()
    {
        // TODO: Add the spawn animation and/or logic.
    }

    /// <summary>
    /// Executed when the object in removed or dispawned.
    /// </summary>
    public void OnDispawn()
    {
        // TODO: Add the spawn animation and/or logic.
    }
}