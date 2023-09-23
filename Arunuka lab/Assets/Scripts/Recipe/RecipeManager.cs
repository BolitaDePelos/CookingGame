using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// Manages all the recipes in the game.
/// </summary>
public class RecipeManager : SingletonMonobehaviour<RecipeManager>
{
    [SerializeField] private List<Recipe> recipes;
    [ReadOnly] [SerializeField] private Recipe currentRecipe;
    [SerializeField] [Range(0, 5)] private float createDishIntervalSeconds;

    [Header("Reaction")] [SerializeField] private GameObject foodEmotionPrefab;

    private float _secondsElapsedToCreateDish;
    private List<(string, int)> _recipeHistory = new();

    private void Start()
    {
        InitializeRecipeHistory();
    }

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        if (currentRecipe != null)
            return;

        _secondsElapsedToCreateDish += Time.deltaTime;
        if (_secondsElapsedToCreateDish > createDishIntervalSeconds)
            CreateDish();
    }


    /// <summary>
    /// Creates a new dish to prepare.
    /// </summary>
    private void CreateDish()
    {
        currentRecipe = recipes.GetRandom();
        PlateManager.Instance.SpawnPlate(currentRecipe.platePrefab, currentRecipe);

        //TODO: Display something when creating the dish.
        //
    }

    /// <summary>
    /// Finish the plate and post the result in the screen.
    /// </summary>
    public void FinishDish(RecipeResult recipeResult, Vector3 iconSpawnPosition)
    {
        int totalScore = 0;

        List<Ingredients> ingredientsServed = recipeResult.foodResults
            .Select(foodResult => foodResult.ingredient)
            .Distinct()
            .ToList();

        foreach (Ingredients ingredient in ingredientsServed)
        {
            ExpectedFoodResult expectedResult = currentRecipe.expectedFoodResult.FirstOrDefault(
                expected => expected.ingredient == ingredient);

            List<FoodResult> foodResultList = recipeResult.foodResults
                .Where(foodResult => foodResult.ingredient == ingredient)
                .ToList();

            if (expectedResult == null || expectedResult.piecesAmount < foodResultList.Count())
                continue;

            double totalByIngredient = foodResultList
                .Select(
                    foodResult => expectedResult.expectedFoodResultByState.FirstOrDefault(
                        state => state.cookState == foodResult.cookState))
                .Where(expectedByState => expectedByState != null)
                .Average(expectedByState => expectedByState.score);

            totalScore += (int) Math.Round(totalByIngredient);
        }

        GameObject foodEmotion = Instantiate(foodEmotionPrefab, iconSpawnPosition, Quaternion.identity);
        foodEmotion.GetComponent<FoodIcon>().SetIcon(currentRecipe.GetEmotionFor(totalScore));
        GameManager.Instance.AddScore(totalScore);

        _recipeHistory ??= new List<(string, int)>();
        _recipeHistory.Add(new ValueTuple<string, int>(currentRecipe.title, totalScore));

        currentRecipe = null;
        _secondsElapsedToCreateDish = 0.0f;
    }

    /// <summary>
    /// Gets the recipe history.
    /// </summary>
    /// <returns></returns>
    public List<(string, int)> GetRecipeHistory()
    {
        return _recipeHistory;
    }

    /// <summary>
    /// Gets the recipe history serialized as json.
    /// </summary>
    public string GetRecipeHistoryJson()
    {
        return JsonConvert.SerializeObject(_recipeHistory);
    }

    /// <summary>
    /// Initialize the saved values in the recipe history.
    /// </summary>
    private void InitializeRecipeHistory()
    {
        if (!SaveProperties.IsSaved())
            return;

        try
        {
            string jsonHistory = PlayerPrefs.GetString(SaveProperties.RecipeHistoryProperty);
            _recipeHistory = JsonConvert.DeserializeObject<List<(string, int)>>(jsonHistory);

            Debug.Log(jsonHistory);
        }
        catch
        {
            Debug.Log("Could not initialize recipe history.");
        }
    }
}