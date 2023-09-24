using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDayManager : SingletonMonobehaviour<EndDayManager>
{
    [SerializeField] private Animator endDayCanvasAnimator;
    [SerializeField] private TextMeshProUGUI moneyGainedText;
    private static readonly int Appearing = Animator.StringToHash("Appearing");
    [SerializeField] [Scene] private int shopSceneInt;

    /// <summary>
    /// Makes appear the end of the day screen.
    /// </summary>
    public void MakeAppear()
    {
        endDayCanvasAnimator.SetTrigger(Appearing);
        moneyGainedText.text = "$" + PlayerPrefs.GetInt(SaveProperties.TodayMoneyProperty, 0);

        Player.Instance.SetCanMove(false);
    }

    public void OnContinueButton()
    {
        RecipeManager.Instance.StartNewDay();
        SceneManager.LoadScene(shopSceneInt);
    }
}