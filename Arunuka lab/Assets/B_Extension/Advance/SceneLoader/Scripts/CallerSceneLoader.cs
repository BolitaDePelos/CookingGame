using B_Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace B_Extensions.SceneLoader
{
    public class CallerSceneLoader : MonoBehaviour
    {
        public static List<string> listScenes = new List<string>();
        [ListToEnumEditor(typeof(CallerSceneLoader), nameof(listScenes))]
        public string sceneName;
        SceneLoader sceneLoader = null;

        private void Start() => sceneLoader = SceneLoader.Instance;
        public void LoadScene() => sceneLoader.LoadScene(sceneName);
        public void ReloadScene() => sceneLoader.ReloadScene();
        public void Pause() => sceneLoader.Pause(true);
        public void Unpause() => sceneLoader.Pause(false);



        #region EditorMethods
        private void OnValidate()
        {
            ReadScenes();
        }

        [ContextMenu("Read Scenes")]
        public void ReadScenes()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            listScenes.Clear();

            for (int i = 0; i < sceneCount; i++)
            {
                listScenes.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            }
        }
        #endregion
    }
}
