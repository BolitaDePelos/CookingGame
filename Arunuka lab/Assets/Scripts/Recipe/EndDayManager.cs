using System.Collections;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDayManager : SingletonMonobehaviour<EndDayManager>
{
    [SerializeField] private Animator endDayCanvasAnimator;
    [SerializeField] private TextMeshProUGUI moneyGainedText;
    [SerializeField] [Scene] private int shopSceneInt;
    [SerializeField] private Animator fadeOutAnimator;

    private static readonly int Appearing = Animator.StringToHash("Appearing");
    private static readonly int Start = Animator.StringToHash("Start");

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
        StartCoroutine(GoToShopScene());
    }

    private IEnumerator GoToShopScene()
    {
        fadeOutAnimator.SetTrigger(Start);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(shopSceneInt);
    }
}