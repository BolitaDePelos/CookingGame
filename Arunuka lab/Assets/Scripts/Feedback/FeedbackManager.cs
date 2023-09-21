using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour{

    [SerializeField]
    private bool inStove;
    [SerializeField]
    private bool inPlate;
    [SerializeField]
    private bool onFire;
    [SerializeField]
    private string foodState;

    private float time;
    [SerializeField]
    private int seconds;

    [SerializeField]
    private List<Sprite> emojis;
    [SerializeField]
    private  Sprite  indexEmoji;
    [Header("Feedback Values Assignment")]

    [SerializeField]
    private int rawLevel=12;
    [SerializeField]
    private int cookedLevel=20;

    [Header("UI Assignment")]
    [SerializeField]
    private Image finalEmoji;
    [SerializeField]
    private TextMeshProUGUI fryValueText;
    [SerializeField]
    private GameObject panel;

    private void Update(){
        if (!inPlate)
        {
            if (inStove)
            {
                if (!onFire)
                {
                    time += Time.deltaTime;
                    seconds = Convert.ToInt32(time % 60);

                    if (seconds > 0 && seconds <= rawLevel)
                        foodState = "Raw";

                    if (seconds > rawLevel + 1 && seconds <= cookedLevel)
                        foodState = "Cooked";

                    if (seconds > cookedLevel)
                    {
                        foodState = "Burn";
                        onFire = true;
                    }
                    SetValue(seconds);

                }

                else
                {
                    time -= Time.deltaTime;
                    seconds = Convert.ToInt32(time % 60);
                    if (seconds == 0)
                    {
                        SetValue(seconds);
                        inStove = false;
                        OnActionEnded();
                    }

                }

            }
            else
            {
                SetValue(seconds);

            }
        }
        else { 
            OnActionEnded(); 
        }
        
       
       
    }

    public void SetValue(int fry) {
        if (fry >= 0 && fry <= rawLevel - 2|| foodState == "Burn")
            FeedbackEmotion("sad");
        if (fry >= rawLevel -1 && fry <= rawLevel + 2)
            FeedbackEmotion("noBad");
        if (fry >= cookedLevel -5 && fry <= cookedLevel || foodState == "Cooked")
            FeedbackEmotion("happy");
    }
    public void FeedbackEmotion(string emotion){
        switch (emotion){            
            case "sad":
                indexEmoji = emojis[0];
                break;

            case "noBad":
                indexEmoji = emojis[1];
                break;

            case "happy":
                indexEmoji = emojis[2];
                break;
            default:
                indexEmoji = emojis[1];
                break;
        }
    }

    public void FeedbackSetter() {
        finalEmoji.sprite = indexEmoji;
        if (foodState == "Burn")
            fryValueText.text ="5 / 20";
        else
            fryValueText.text = seconds.ToString()+" / 20";
    }
    public void OnActionEnded() {
        FeedbackSetter();
        panel.SetActive(true);

    }
}
