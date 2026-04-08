using UnityEditor;
using UnityEngine;

namespace B_Extensions.HierarchyStates
{
    public class HierarchyEditor : EditorWindow
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Color Hierarchy", false, 12)]
        public static void ShowWindow()
        {
            GetWindow<HierarchyEditor>("HierarchyEditor");
        }

        [MenuItem("GameObject/State/Add %e", false, 11)]
        public static void AddState()
        {
            Selection.activeGameObject.AddComponent(typeof(StateSetter));
        }

        [MenuItem("GameObject/State/Remove", false, 11)]
        public static void RemoveState()
        {
            DestroyImmediate(Selection.activeGameObject.GetComponent<StateSetter>());
        }

        private Color bufferColor = new Color(0.6f, 0.2f, 0.6f, 1f);
        private void OnGUI()
        {
            bufferColor = EditorGUILayout.ColorField("Color to " + Selection.activeGameObject.name, bufferColor);

            if (GUILayout.Button("Apply Color"))
            {
                var selections = Selection.gameObjects;

                foreach (var item in selections)
                {
                    if (!item.GetComponent<ColorHierarchySetter>())
                        item.AddComponent(typeof(ColorHierarchySetter));
                    item.GetComponent<ColorHierarchySetter>().colorInHierarchy = bufferColor;
                }

                this.Close();
            }

            if (GUILayout.Button("Remove Color"))
            {
                var selectionsTwo = Selection.gameObjects;

                foreach (var item in selectionsTwo)
                {
                    if (item.GetComponent<ColorHierarchySetter>())
                    {
                        DestroyImmediate(item.GetComponent<ColorHierarchySetter>());
                        this.Close();
                    }
                }
            }
        }
#endif
    }
}