using B_Extensions.ClassesExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace B_Extensions
{
    [RequireComponent(typeof(Button))]
    public class BaseButtonAttendant : MonoBehaviour
    {
        protected Button buttonComponent => GetComponent<Button>();
        protected UnityAction[] bufferAcctions;
        public void Click() => buttonComponent.onClick?.Invoke();

        protected void WriteBufferActions()
        {
            bufferAcctions = buttonComponent.onClick.GetPersistentEventListeners();
            buttonComponent.onClick = null;
            buttonComponent.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
        }

        protected void InvokeBufferActions()
        {
            foreach (var item in bufferAcctions)
            {
                item.Invoke();
            }
        }
    }
}