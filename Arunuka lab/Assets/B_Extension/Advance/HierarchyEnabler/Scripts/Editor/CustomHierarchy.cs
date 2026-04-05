using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

namespace B_Extensions.HierarchyStates
{

    [InitializeOnLoad]
    class CustomHierarchy
    {

#if UNITY_EDITOR

        public static StateSetter[] stateObjects;
        static ColorHierarchySetter[] colorObjects;
        static CustomHierarchy()
        {
            EditorApplication.update += UpdateCB;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        static void UpdateCB()
        {
            stateObjects = Resources.FindObjectsOfTypeAll<StateSetter>();
            colorObjects = Resources.FindObjectsOfTypeAll<ColorHierarchySetter>();
        }

        public static void SearchEnablers()
        {
            foreach (var item in stateObjects)
            { 
                item.EnableOnHierarchy();
                EditorUtility.SetDirty(item);
                EditorSceneManager.MarkSceneDirty(item.gameObject.scene);
            }
        }

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            DrawHierarchyColor(instanceID, selectionRect);
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            DrawDisabledCompo(selectionRect,obj);
            DrawEnabledCompo( selectionRect, obj);
            DrawEnablerRemains(instanceID, selectionRect);
        }

        private static void DrawHierarchyColor(int _instanceID, Rect _selectionRect)
        {
            if (!TryGetColorSetter()) return;

            ColorHierarchySetter colorItem = null;

            foreach (ColorHierarchySetter item in colorObjects)
            {
                if (item != null && item.gameObject.GetInstanceID().Equals(_instanceID))
                {
                    colorItem = item;
                    break;
                }
            }

            if (colorItem != null)
            {
                Rect rectLeft = _selectionRect;
                Rect toDrawOne = rectLeft;
                Rect toDrawFill = rectLeft;
                Rect toDrawThree = rectLeft;

                toDrawOne.height =  toDrawThree.height  = 1;
                toDrawFill.height = 8;
                toDrawOne.x += 0;
                toDrawFill.x += 50;
                toDrawThree.x += 0;

                toDrawOne.y += 0;
                toDrawFill.y += 4;
                toDrawThree.y += 16;

                rectLeft.width = _selectionRect.width*2;
                rectLeft.height = 2;

                var colorFull = colorItem.colorInHierarchy;
                colorFull.a = 0.08f;

                EditorGUI.DrawRect(toDrawOne, colorItem.colorInHierarchy);
                EditorGUI.DrawRect(toDrawFill, colorFull);
                EditorGUI.DrawRect(toDrawThree, colorItem.colorInHierarchy);
            }
        }

        private static void DrawEnablerRemains(int _instanceID, Rect _selectionRect)
        {
            if (!TryGetStateSetter()) return;

            StateSetter stateItem = null;

            if (TryGetStateSetter())
            {
                foreach (StateSetter item in stateObjects)
                {
                    if (item != null && item.gameObject.GetInstanceID().Equals(_instanceID))
                    {
                        stateItem = item;
                        break;
                    }
                }
            }

            if (stateItem != null)
            {
                Rect r = new Rect(_selectionRect);
                r.height = 16;
                r.x = _selectionRect.xMax -20;
                r.width = 20;

                if (GUI.Button(r, stateItem.stateEnable ? "O" : ""))
                    stateItem.stateEnable = !stateItem.stateEnable;
            }
        }

        private static void DrawEnabledCompo(Rect selectionRect, GameObject obj)
        {
            // Obtén el objeto asociado al ID
            
            if (obj == null) return;
            string smallText = "";
            GUIStyle smallTextStyle = null;

            if (obj.GetComponent<OnEnableEventHandler>() != null)
            {
                smallText = "Enab";

                smallTextStyle = new GUIStyle(EditorStyles.label)
                {
                    fontSize = 7,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = Color.cyan}
                };
            }

            if (string.IsNullOrEmpty(smallText))
                return;

            Rect textRect = new Rect(selectionRect.xMax - 50, selectionRect.y+4, 50, selectionRect.height);            
            GUI.Label(textRect, smallText, smallTextStyle);
        }

        private static void DrawDisabledCompo(Rect selectionRect, GameObject obj)
        {
            if (obj == null) return;
            string smallText = "";

            GUIStyle smallTextStyle = null;

            if (obj.GetComponent<OnDisableEventHandler>() != null)
            {
                smallText = "Disa";

                smallTextStyle = new GUIStyle(EditorStyles.label)
                {
                    fontSize = 7,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = Color.black }
                };
            }

            if (string.IsNullOrEmpty(smallText))
                return;

            // text pos
            Rect textRect = new Rect(selectionRect.xMax - 50, selectionRect.y-4, 50, selectionRect.height);
            GUI.Label(textRect, smallText, smallTextStyle);
        }


        static bool TryGetStateSetter()
        {
            if (stateObjects == null)
                return false;
            else
                return true;
        }

        static bool TryGetColorSetter()
        {
            if (colorObjects == null)
                return false;
            else
                return true;
        }
#endif
    }
}