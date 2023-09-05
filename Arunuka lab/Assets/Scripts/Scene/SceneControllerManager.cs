using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerManager : SingletonMonobehaviour<SceneControllerManager>
{

    private bool isFading;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private CanvasGroup faderCanvasGroup = null;
    [SerializeField] private Image faderImagen = null;
    public SceneName StaringsceneName;

    private IEnumerator Fade(float finalAlpha)
    {
        //Set the fading flag to true so the FadeAndSwitchScenes corotine wont be called againg
        isFading = true;

        //Make sure the canvasgroup blocks raycasts into the scene so no more input can be accepted
        faderCanvasGroup.blocksRaycasts = true;

        //Calculate how fast the cnavasgroup should fade based on its current alpha, its final and how long it has to change between the two
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        //While the canvas group hasnt reached the final alpha yet...
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            //..Move the alpha towards its target alpha
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha,finalAlpha,fadeSpeed*Time.deltaTime);

            //Wait for a frame then continue
            yield return null;
        }

        //Set the flag to false since the fade has finished
        isFading=false;

        //Stop the canvasgroup from blocking raycasts so input is no longer ignored
        faderCanvasGroup.blocksRaycasts=false;

    }







    //This is the coroutine where the building blocks of the script are put together

    private IEnumerator FadeAndSwitchScenes(string sceneName, Vector3 spawnPosition)
    {
        //Call before scene unload fade out event
        GameEventsManager.instance.SceneLoadEvents.CallBeforeSceneUnloadFadeOutEvent();


        //Start fading and wait for it to finish before continuing
        yield return StartCoroutine(Fade(1f));

        //Call before scene unload Event
        GameEventsManager.instance.SceneLoadEvents.CallBeforeSceneUnloadEvent();

        //Unload the current active scene
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //Start loading the given scene and wait for it to finish
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        //Call after scene load event
        GameEventsManager.instance.SceneLoadEvents.CallAfterSceneLoadEvent();

        //Start fading back in and wait for it to finish before exiting the function.
        yield return StartCoroutine(Fade(0f));

        //Call after scene load fade in event
        GameEventsManager.instance.SceneLoadEvents.CallAfterSceneLoadFadeInEvent();


    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        // Allow the given scene to load over several frames and add it to the already loded scenes
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        //Find the scene that was most recently loaded
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        //Set the newly loaded scene as the active scene
        SceneManager.SetActiveScene(newlyLoadedScene);
    }



    public void FadeAndLoadScene(string sceneName, Vector3 spawnPosition)
    {
        //If a fade isnt happening then start fading and switching scene
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName, spawnPosition));
        }

    }


}
