using UnityEngine;
using UnityEngine.UI;

namespace B_Extensions
{
    [RequireComponent(typeof(Toggle))]

    public class BaseToggleAttendant : MonoBehaviour
    {
        protected Toggle toggleComponent => GetComponent<Toggle>();
        public void Click(bool value) => toggleComponent.onValueChanged?.Invoke(value);

    }
}