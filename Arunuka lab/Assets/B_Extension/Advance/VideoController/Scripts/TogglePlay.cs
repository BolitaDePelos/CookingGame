using B_Extensions;
using UnityEngine;
using UnityEngine.Events;
public class TogglePlay : BaseToggleAttendant
{
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    private void Start()
    {
        toggleComponent.image.sprite = (toggleComponent.isOn) ?_spriteOn:_spriteOff;
        toggleComponent.onValueChanged.AddListener(Display);
    }

    private void Display(bool result) 
    {
        toggleComponent.image.sprite = (result) ? _spriteOn : _spriteOff;
    }

    public void Configure(UnityAction<bool> events)
    {
        toggleComponent.onValueChanged.AddListener(events);
    }
}
