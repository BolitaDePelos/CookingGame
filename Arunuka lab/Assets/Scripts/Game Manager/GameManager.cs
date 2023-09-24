using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [ReadOnly] [SerializeField] private int currentScore;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        //TODO: Need a resolution settings option screen
        //
        const int displayResWidth = 1920;
        const int displayResHeight = 1080;
        const bool fullScreen = false;
        PlayerPrefs.SetInt("Screenmanager Resolution Width", displayResWidth);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", displayResHeight);
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(displayResWidth, displayResHeight, fullScreen);
        InitializeSavedValues();
    }

    /// <summary>
    /// Adds value to the score.
    /// </summary>
    public void AddScore(int score)
    {
        currentScore += score;

        if (scoreText == null)
            return;
        MoneyUpdater moneyUpdater = new MoneyUpdater();
        moneyUpdater.UpdateMoney(currentScore);
        scoreText.text = "$" + currentScore;
    }

    /// <summary>
    /// Loads all the saved values from <see cref="PlayerPrefs"/>
    /// </summary>
    private void InitializeSavedValues()
    {
        if (!SaveProperties.IsSaved())
            return;

        AddScore(PlayerPrefs.GetInt(SaveProperties.ScoreProperty, 0));
        // TODO: Load the other values.
    }

    /// <summary>
    /// Save all the savable values into <see cref="PlayerPrefs"/>
    /// </summary>
    public void Save()
    {
        PlayerPrefs.SetInt(SaveProperties.IsSavedProperty, 1);
        PlayerPrefs.SetInt(SaveProperties.ScoreProperty, currentScore);

        RecipeManager.Instance.Save();
    }
}