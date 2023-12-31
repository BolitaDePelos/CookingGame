using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    private AudioManager audioManager;

    [Header("Level to load")]
    public string _LevelLoad = SceneName.MainMenu.ToString();

    private void Start()
    {
        PauseMenuUI.SetActive(false);
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (InputManager.GetInstance().GetPauseMenuPressed())
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioManager.PlayPauseSound();
        Cursor.visible = true;
    }

    public void Continue()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioManager.PlayPauseSound();
        Cursor.visible = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneControllerManager.Instance.LoadNextLevel(_LevelLoad);
        Time.timeScale = 1f;
    }

}
