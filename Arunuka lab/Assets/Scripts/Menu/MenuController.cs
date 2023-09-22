using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumenTextValue= null;
    [SerializeField] private Slider volumenSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1.0f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;


    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;




    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    


    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Level to load")]
    public string _newGameLevel = SceneName.Scene_Level_1.ToString();
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Anim")]
    private bool Menu;
    private bool Fade;
    public Animator anim;

    public GameObject CanvasAnyKey;
    public GameObject FadeCanvas;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Menu = false;
        Fade = false;
        CanvasAnyKey.SetActive(true);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for(int i = 0;i< resolutions.Length; i++)
        {
            string option = resolutions[i].width +" x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    private void Update()
    {
        PressAnyKey();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void NewGameDialoYes()
    {
        SceneControllerManager.Instance.LoadNextLevel(_newGameLevel);
        Fade = true;
        anim.SetTrigger("Fade");
        FadeCanvas.SetActive(true);

    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SaveLevel");
            SceneControllerManager.Instance.LoadNextLevel(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    public void SetVolume(float volumen)
    {
        AudioListener.volume = volumen;
        volumenTextValue.text = volumen.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {

            //Reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height,Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }



        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumenSlider.value = defaultVolume;
            volumenTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }
   
    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);

        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));

        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }


    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);

    }

    public void PressAnyKey()
    {
        if (InputMainMenu.GetInstance().GetAnyPressed() && CanvasAnyKey.activeSelf)
        {
            Menu = true;
            anim.SetBool("Menu", true);
            CanvasAnyKey.SetActive(false);
            FadeCanvas.SetActive(true);
        }
    }

}
