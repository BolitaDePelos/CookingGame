using UnityEngine;
using UnityEngine.UI;


namespace B_Extensions.DebugHandler
{ 
    public class CardDebug : MonoBehaviour
    {
        [SerializeField] Text textStacktrace;
        public void DrawText(string text, LogType type) 
        {
            textStacktrace.text = text;
            Tint(type);
        }

        private void Tint(LogType type) 
        {
            switch (type)
            {
                case LogType.Log:
                    textStacktrace.color = Color.white;
                    break;
                case LogType.Warning:
                    textStacktrace.color = Color.yellow;
                    break;
                case LogType.Error:
                    textStacktrace.color = Color.red;
                    break;
                default:
                    break;
            }

        }
    }
}
