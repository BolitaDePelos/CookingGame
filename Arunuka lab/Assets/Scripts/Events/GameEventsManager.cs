using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : SingletonMonobehaviour<GameEventsManager>
{
    public InputEvents InputEvents;
    public SceneLoadEvents SceneLoadEvents;

    private new void Awake()
    {
        base.Awake();

        // initialize all events
        InputEvents = new InputEvents();
        SceneLoadEvents = new SceneLoadEvents();
    }

}
