using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



namespace B_Extensions
{
#if UNITY_EDITOR

    public class WindowColorPalette : EditorWindow
    {
        [MenuItem("B_Extensions/Palette")]
        public static void ShowWindow()
        {
            //PlayerPrefs.SetString(KeyStorage.PlayersTest, "Hola Mundo");
            EditorWindow window = GetWindow(typeof(WindowColorPalette));
            window.Show();
            bufferColor = new Color[7];
            List<string> jsonPaths = SearchPath();
            CargarPaleta(jsonPaths[0]);
            if (jsonPaths.Count == 0)
            {
                Debug.LogError("No se encontraron archivos paletteColor.json en Assets.");
            }
        }

        private static List<string> SearchPath()
        {
            string[] guids = AssetDatabase.FindAssets("t:TextAsset");
            List<string> jsonPaths = new List<string>();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.EndsWith("paletteColor.json"))
                {
                    jsonPaths.Add(path);
                }
            }
            return jsonPaths;
        }

        private static Color[] bufferColor;


        private void OnGUI()
        {
            for (int i = 0; i < bufferColor.Length; i++)
            {
                GUILayout.BeginHorizontal();
                bufferColor[i] = EditorGUILayout.ColorField("Color "+(i+1), bufferColor[i]);

                if (GUILayout.Button(ColorUtility.ToHtmlStringRGB(bufferColor[i]))) 
                {
                    EditorGUIUtility.systemCopyBuffer = ColorUtility.ToHtmlStringRGB(bufferColor[i]);
                    ShowNotification(new GUIContent("¡Copiado!"), 0.4f);
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("guardar"))
            {
                List<string> jsonPaths = SearchPath();
                GuardarPaleta(jsonPaths[0]);
                ShowNotification(new GUIContent("✅ ¡Guardado!"), 0.4f);
            }

            this.Repaint();
        }

        static void CargarPaleta(string foundPath)
        {
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(foundPath);

            if (textAsset == null)
            {
                Debug.LogError("error load json");
                return;
            }

            PaletaColoresWrapper paleta = JsonUtility.FromJson<PaletaColoresWrapper>(textAsset.text);

            if (paleta.colores != null)
            {
                //Debug.Log($"Paleta cargada con {paleta.colores.Length} colores:");

                for ( int i = 0; i < paleta.colores.Length; i++)
                {
                    Color unityColor;

                    if (ColorUtility.TryParseHtmlString(paleta.colores[i].hexCode, out unityColor))
                    {
                        //Debug.Log($"Nombre: , Hex: {paleta.colores[i].hexCode}, Unity Color: {unityColor}");
                        bufferColor[i] = unityColor;
                    }
                }

            }
        }

        static void GuardarPaleta(string foundPath)
        {
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(foundPath);

            PaletaColoresWrapper wrapper = new PaletaColoresWrapper();

            wrapper.colores = new ColorData[7];

            for (int i = 0; i < wrapper.colores.Length; i++)
            {
                wrapper.colores[i] = new ColorData { hexCode = "#" + ColorUtility.ToHtmlStringRGBA(bufferColor[i]) };
            }

            string json = JsonUtility.ToJson(wrapper, true);

            File.WriteAllText(foundPath, json);

            AssetDatabase.ImportAsset(foundPath);
        }

        [Serializable]
        public class ColorData
        {
            public string hexCode;
        }

        [Serializable]
        public class PaletaColoresWrapper
        {
            public ColorData[] colores;
        }

}
#endif
}
