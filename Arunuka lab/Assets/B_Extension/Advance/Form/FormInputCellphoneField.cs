using System;
using TMPro;
using UnityEngine;

public class FormInputCellphoneField : MonoBehaviour, IFormInput
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string errorType = "No es un número de correo valido";
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    private void Start() => inputField.onValueChanged.AddListener(CheckInput);

    private void CheckInput(string arg0)
    {
        if (CheckComplete())
            OnUpdateState?.Invoke(arg0);
        else
            OnError?.Invoke(errorType);
    }

    public bool CheckComplete()
    {
        return inputField.text.Length.Equals(10);
    }

    public object GetValue()
    {
        return inputField.text;
    }
}

