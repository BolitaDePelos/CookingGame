using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class FormInputEmail : MonoBehaviour, IFormInput
{
    [SerializeField] private TMP_InputField inputField;
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    private void Start() => inputField.onValueChanged.AddListener(CheckInput);

    private void CheckInput(string arg0)
    {
        if (CheckComplete())
            OnUpdateState?.Invoke(arg0);
        else
            OnError?.Invoke("No es un correo valido");
    }

    public bool CheckComplete()
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(inputField.text,pattern) && inputField.text.Length>9;
    }
    public object GetValue() => inputField.text;
}
