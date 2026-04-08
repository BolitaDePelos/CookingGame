using System;
using TMPro;
using UnityEngine;

public class FormDropdown : MonoBehaviour, IFormInput
{
    [SerializeField] private TMP_Dropdown dropdown;

    public event Action<object> OnUpdateState;
    public event Action<string> OnError;

    void Start()
    {
        dropdown.onValueChanged.AddListener(InvokeChange);
    }

    private void InvokeChange(int arg0) => OnUpdateState?.Invoke(Value);

    public bool CheckComplete() => true;

    public object GetValue() => Value;

    private string Value => dropdown.options[dropdown.value].text;
}