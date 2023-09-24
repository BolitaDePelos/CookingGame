using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the ingredients placed in the plate.
/// </summary>
public class Plate : MonoBehaviour
{
    [Header("Food")] [SerializeField] private List<GameObject> foodOnPlate = new();
    [SerializeField] private float destroyAfterSeconds = 1.0f;
    [SerializeField] private PlateHoverable myHoverable;
    [SerializeField] private TextMeshPro hoverableText;
    [SerializeField] private string textToDisplay;

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

    public bool IsEmpty()
    {
        return !foodOnPlate.Any();
    }

    public void FinishPlate()
    {
        if (!foodOnPlate.Any())
            return;

        List<FoodResult> foodResults = new();
        foodOnPlate.ForEach(
            foodObject =>
            {
                var pickable = foodObject.GetComponent<IPickable>();
                pickable.SetIsPickable(false);

                if (!foodObject.TryGetComponent(out Food food))
                    return;

                foodObject.transform.parent = transform;

                FoodResult foodResult = new()
                {
                    ingredient = food.IngredientType,
                    cookState = food.GetFoodState(),
                    cookDurationSeconds = food.GetCookedTimeSeconds()
                };

                foodResults.Add(foodResult);
            });


        myHoverable.SetHoverable(false);

        // TODO: Improve code so the plate also knows what recipe is serving.
        //
        RecipeManager.Instance.FinishDish(
            new RecipeResult {foodResults = foodResults},
            transform.position);

        _mustDestroy = true;
    }

    /// <summary>
    /// Called when the object is spawned.
    /// </summary>
    public void OnSpawn(Recipe recipe)
    {
        textToDisplay = textToDisplay.Replace("[Recipe]", recipe.title);
        hoverableText.text = textToDisplay;
    }
}