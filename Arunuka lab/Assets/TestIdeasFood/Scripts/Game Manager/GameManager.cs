using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    //Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();

        //TODO: Need a resolution settings option screen

        var displayResWidth = 1920;
        var displayResHeight = 1080;
        var fullScreen = false;

        PlayerPrefs.SetInt("Screenmanager Resolution Width", displayResWidth);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", displayResHeight);
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(displayResWidth, displayResHeight, fullScreen);
    }


}
