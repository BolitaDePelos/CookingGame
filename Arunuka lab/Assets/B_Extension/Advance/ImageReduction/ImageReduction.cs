using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ImageReduction : EditorWindow
{
    [MenuItem("B_Extensions/Compress Selected Images #^h")] //
    private static void GetPath()
    {
        Object[] selectedObject = Selection.objects;
        List<string> pathsImages = new List<string>();

        if (selectedObject.Length == 0)
        {
            UnityEngine.Debug.Log("No hay imagenes selccionads");
            return;
        }

        foreach (var obj in selectedObject)
        {
            if (obj is Texture2D)
            {
                var elemn = AssetDatabase.GetAssetPath(obj);
                var pathNew = Path.Combine(Application.dataPath, elemn);
                var fales = pathNew.Replace("\\Assets", "");
                var nmalized = fales.Replace('\\', '/');
                pathsImages.Add($"\"{fales}\"");
                UnityEngine.Debug.Log("Imagen Enviada a comprimir: " + obj.name);
            }
        }

        string[] guids = AssetDatabase.FindAssets("image_multi_4");
        if (guids.Length > 0)
        {
            foreach (var item in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.Contains("image_multi_4.exe") && !path.Contains("deps"))
                {
                    string exePath = Path.Combine(Application.dataPath,"..",path);
                    string arguments = string.Join(" ", pathsImages);

                    if(pathsImages.Count==0)
                        return;

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = arguments,
                        UseShellExecute = true,     // permite mostrar la consola
                        CreateNoWindow = false,     //  Muestra la ventana
                        RedirectStandardOutput = false, //  deactive redirection
                        RedirectStandardError = false,  //  same but with errors
                    };

                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        process.WaitForExit(); // Espera a que termine (opcional)
                    }
                }
            }
        }
    }
}
#endif
