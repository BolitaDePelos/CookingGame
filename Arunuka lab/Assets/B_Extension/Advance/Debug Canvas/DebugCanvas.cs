using System;
using System.Collections.Generic;
using UnityEngine;


namespace B_Extensions.DebugHandler
{ 
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] CardDebug prototype = null;
        [SerializeField] private Transform content;
        List<CardDebug> cardsLog = new List<CardDebug>();

        private void Awake()
        {
            Application.logMessageReceived += ReceiveLog;
        }

        private void ReceiveLog(string condition, string stackTrace, LogType type)
        {
            var clone = Instantiate(prototype, content);
            clone.DrawText(condition+"_____"+stackTrace,type);
            if(cardsLog.Count<200)
                cardsLog.Add(clone);
        }

        public void Clean() 
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= ReceiveLog;
        }
    }
}
