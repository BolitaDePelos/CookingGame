using B_Extensions;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ToggleEventsBasic : BaseToggleAttendant
{
    [SerializeField] public UnityEvent OnOn;
    [SerializeField] public UnityEvent OnOff;
    void Start() => toggleComponent.onValueChanged.AddListener(InvokeEvent);
    private void InvokeEvent(bool arg0)
    {
        if (arg0)
            OnOn?.Invoke();
        else
            OnOff?.Invoke();
    }
}
