using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class FormInputNameField : MonoBehaviour, IFormInput
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string errorMessageLimit = "Los Nombres o Apellidos muy cortos";
    [SerializeField] private string errorMessageNames = "Debes introducir tu nombre completo";
    [SerializeField] private string errorMessageSpecialChars = "Tu nombre no debe tener caracteres especiales";
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    private void Start() => inputField.onValueChanged.AddListener(CheckInput);

    private void CheckInput(string arg0)
    {
        if (CheckComplete())
            OnUpdateState?.Invoke(arg0);
            
    }

    public bool CheckComplete()
    {
        // \b for separate words
        // \p{L} letters witch accents
        //' O'Connor is a word
        //- Jean-Luc is a word
        MatchCollection matches = Regex.Matches(inputField.text, @"\b[\p{L}'\-]+\b");

        if (matches.Count <2)
        {
            OnError?.Invoke(errorMessageNames);
            return false;
        }

        if (inputField.text.Length <9)
        {
            OnError?.Invoke(errorMessageLimit);
            return false;
        }

        string patternSpecials = @"^[\p{L}\-' ]+$";

        if (!Regex.IsMatch(inputField.text,patternSpecials))
        {
            OnError?.Invoke(errorMessageSpecialChars);
            return false;
        }

        return true;
    }

    public object GetValue() => inputField.text;
}
