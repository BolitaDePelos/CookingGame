using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerManager : Singleton<SceneControllerManager>
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel(string _LevelLoad)
    {
       StartCoroutine(loadLevel(_LevelLoad));
    }

    IEnumerator loadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        var  progress = SceneManager.LoadSceneAsync(levelIndex);

        yield return new WaitUntil(()=>progress.isDone == true);
        print("Do Something");

        //SceneManager.LoadScene(levelIndex);

    }

}
