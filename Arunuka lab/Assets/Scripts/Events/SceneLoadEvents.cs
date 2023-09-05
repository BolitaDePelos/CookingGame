using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadEvents
{
    /// <summary>
    /// Before Scene Unload Fade Out Event
    /// </summary>

    public event Action BeforeSceneUnloadFadeOutEvent;
    public void CallBeforeSceneUnloadFadeOutEvent()
    {
        if (BeforeSceneUnloadFadeOutEvent != null)
        {
            BeforeSceneUnloadFadeOutEvent();
        }
    }

    /// <summary>
    /// Before Scene Unload Event
    /// </summary>

    public event Action BeforeSceneUnloadEvent;
    public void CallBeforeSceneUnloadEvent()
    {
        if (BeforeSceneUnloadEvent != null)
        {
            BeforeSceneUnloadEvent();
        }
    }


    /// <summary>
    /// After Scene loaded Event
    /// </summary>

    public event Action AfterSceneLoadEvent;
    public void CallAfterSceneLoadEvent()
    {
        if (AfterSceneLoadEvent != null)
        {
            AfterSceneLoadEvent();
        }
    }


    /// <summary>
    /// After Scene load Fade in Event
    /// </summary>

    public event Action AfterSceneLoadFadeInEvent;
    public void CallAfterSceneLoadFadeInEvent()
    {
        if (AfterSceneLoadFadeInEvent != null)
        {
            AfterSceneLoadFadeInEvent();
        }
    }

}
