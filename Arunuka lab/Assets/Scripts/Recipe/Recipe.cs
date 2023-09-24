using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "CookingData/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    /// <summary>
    /// Must be unique.
    /// </summary>
    public string title;

    public string description;
    public GameObject platePrefab;

    [MinMaxSlider(0.0f, 100.0f)] public Vector2 scoreForBad;
    [MinMaxSlider(0.0f, 100.0f)] public Vector2 scoreForNormal;
    [MinMaxSlider(0.0f, 100.0f)] public Vector2 scoreForGood;

    public List<ExpectedFoodResult> expectedFoodResult;

    /// <summary>
    /// Gets how good was the recipe based on a given score.
    /// </summary>
    public Emotion GetEmotionFor(float score)
    {
        if (scoreForBad.InRange(score))
            return Emotion.Bad;

        if (scoreForNormal.InRange(score))
            return Emotion.Normal;

        return scoreForGood.InRange(score) ? Emotion.Good : Emotion.Bad;
    }
}

/// <summary>
/// Expected food result.
/// </summary>
[Serializable]
public class ExpectedFoodResult
{
    public Ingredients ingredient;
    public int piecesAmount;
    public List<ExpectedFoodResultByState> expectedFoodResultByState;

    [Serializable]
    public class ExpectedFoodResultByState
    {
        public FoodCookState cookState;
        public int score;
    }
}

[Serializable]
public class RecipeResult
{
    public List<FoodResult> foodResults;
}

[Serializable]
public class FoodResult
{
    public Ingredients ingredient;
    public FoodCookState cookState;
    public float cookDurationSeconds;
}