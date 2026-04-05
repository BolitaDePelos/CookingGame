using System;
using UnityEngine;
using UnityEngine.UI;

public class FormToggleInput: MonoBehaviour, IFormInput
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private string errorType;
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    private void Start() => toggle.onValueChanged.AddListener(ValueSave);

    private void ValueSave(bool arg0)
    {
        OnUpdateState?.Invoke(arg0);
    }

    public bool CheckComplete() => toggle.isOn;

    public object GetValue() => toggle.isOn;
}
