using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B_Extensions;
using B_Extensions.SceneLoader;

[RequireComponent(typeof(CallerSceneLoader))]
public class ButtonLoaderScene : BaseButtonAttendant
{
    [SerializeField] bool pauseOnClick = false;
    [SerializeField] bool unpauseOnClick = false;
    CallerSceneLoader callerLoading;
    private void Start()
    {
        buttonComponent.onClick.AddListener(LoadScene);
        callerLoading = GetComponent<CallerSceneLoader>();
    }

    private void LoadScene()
    {
        callerLoading.LoadScene();
        if(pauseOnClick)
            callerLoading.Pause();
        if (unpauseOnClick)
            callerLoading.Unpause();
    }
}