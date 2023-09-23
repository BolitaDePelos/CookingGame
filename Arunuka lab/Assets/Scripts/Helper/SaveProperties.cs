using UnityEngine;

/// <summary>
/// Properties values that will be used to save data.
/// </summary>
public static class SaveProperties
{
    /// <summary>
    /// Property where the score will be saved.
    /// </summary>
    public const string ScoreProperty = "Score";

    /// <summary>
    /// If the game was saved or not.
    /// </summary>
    public const string IsSavedProperty = "IsSaved";

    /// <summary>
    /// All the dishes served.
    /// </summary>
    /// <remarks>
    /// The saving value is a json with structure
    /// <code>
    /// List{(Recipe, int)}
    /// </code>
    /// </remarks>
    public const string RecipeHistoryProperty = "RecipeHistory";

    /// <summary>
    /// Resets the save game.
    /// </summary>
    public static void ResetSave()
    {
        PlayerPrefs.SetInt(IsSavedProperty, 0);
        PlayerPrefs.SetInt(ScoreProperty, 0);
        PlayerPrefs.SetString(RecipeHistoryProperty, "[]");

        // TODO: Reset the other values.
        //
    }

    /// <summary>
    /// If the save is saved or not.
    /// </summary>
    public static bool IsSaved()
    {
        return PlayerPrefs.GetInt(IsSavedProperty, 0) == 1;
    }
}