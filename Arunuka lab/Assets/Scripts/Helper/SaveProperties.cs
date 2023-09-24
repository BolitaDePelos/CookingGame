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
    /// Today money gained.
    /// </summary>
    public const string TodayMoneyProperty = "TodayMoney";

    /// <summary>
    /// How many plates were served today.
    /// </summary>
    public const string PlatesServedToday = "PlatesServedToday";

    /// <summary>
    /// Prefix for the current saved prop.
    /// </summary>
    public const string PrefixCurrentProp = "CURRENT_STORE_";

    public const string CurrentDay = "CurrentDay";

    /// <summary>
    /// Resets the save game.
    /// </summary>
    public static void ResetSave()
    {
        PlayerPrefs.SetInt(IsSavedProperty, 0);
        PlayerPrefs.SetInt(ScoreProperty, 0);
        PlayerPrefs.SetString(RecipeHistoryProperty, "[]");

        PlayerPrefs.SetInt(TodayMoneyProperty, 0);
        PlayerPrefs.SetInt(PlatesServedToday, 0);

        PlayerPrefs.SetInt(CurrentDay, 0);
    }

    /// <summary>
    /// If the save is saved or not.
    /// </summary>
    public static bool IsSaved()
    {
        return PlayerPrefs.GetInt(IsSavedProperty, 0) == 1;
    }
}