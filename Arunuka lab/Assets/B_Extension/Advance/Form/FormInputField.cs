using System;
using TMPro;
using UnityEngine;

public class FormInputField : MonoBehaviour, IFormInput
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string errorType;
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    private void Start()
    {
        inputField.onValueChanged.AddListener(CheckInput);
    }

    private void CheckInput(string arg0)
    {
        if (CheckComplete())
            OnUpdateState?.Invoke(arg0);
    }

    public bool CheckComplete()
    {
        return inputField.text.Length > 6;
    }

    public object GetValue() => inputField.text;
}

