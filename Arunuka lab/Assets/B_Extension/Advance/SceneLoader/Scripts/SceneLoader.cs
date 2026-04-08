using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace B_Extensions.SceneLoader
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [RequireBadInterface(typeof(ILoadingScreen))]
        public MonoBehaviour loadingScreen;
        ILoadingScreen loadingInterface;

        [SerializeField] private float timeBetweenLoad =0f; 

        PauseHandler pauseHandler = null;
        protected override void Awake()
        {
            base.Awake();
            Configure();
        }

        public void Configure() 
        {
            pauseHandler = new PauseHandler();
            loadingInterface = loadingScreen.GetComponent<ILoadingScreen>();
        }

        public void SetLoadingScreen(ILoadingScreen newLoadingScreen) => loadingInterface = newLoadingScreen;
        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public void ReloadScene() => LoadScene(CurrentSceneIndex);
        public void LoadScene(int sceneIndex) => StartCoroutine(CallLoadScene(SceneManager.GetSceneAt(sceneIndex).name));
        public void LoadScene(string sceneName) => StartCoroutine(CallLoadScene(sceneName));
        public void UnloadScene(string sceneName) => StartCoroutine(UnloadProcessAdditive(sceneName));
        public void LoadSceneAdditive(string sceneName) => StartCoroutine(CallLoadScene(sceneName));

        #region Coroutines

        IEnumerator CallLoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            yield return LoadingProcess(sceneName,mode);
        }

        IEnumerator LoadingProcess(string sceneName, LoadSceneMode mode)
        {
            AsyncOperation progress = SceneManager.LoadSceneAsync(sceneName,mode);
            progress.allowSceneActivation = false;
            loadingInterface.OnStartLoading();
            yield return new WaitForSeconds(timeBetweenLoad);
            while (!progress.isDone)
            {
                yield return null;
                loadingInterface.LoadingProgress(progress.progress);

                if (progress.progress >= 0.9f)
                    progress.allowSceneActivation = true;
            }
            loadingInterface.OnEndLoading();
        }

        IEnumerator UnloadProcessAdditive(string sceneName)
        {
            AsyncOperation progress = SceneManager.UnloadSceneAsync(sceneName);
            progress.allowSceneActivation = false;
            loadingInterface.OnStartLoading();
            yield return new WaitForSeconds(timeBetweenLoad);
            while (!progress.isDone)
            {
                yield return null;
                loadingInterface.LoadingProgress(progress.progress);

                if (progress.progress >= 0.9f)
                    progress.allowSceneActivation = true;
            }
            loadingInterface.OnEndLoading();
        }

        #endregion

        #region Static Methods
        public static string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
        #endregion

        public void Quit() => Application.Quit();
        public void Pause(bool pause)=> pauseHandler.Pause(pause);
        public void Pause(float time) => pauseHandler.Pause(time);
        public void Pause() => pauseHandler.Pause();
    }
}
