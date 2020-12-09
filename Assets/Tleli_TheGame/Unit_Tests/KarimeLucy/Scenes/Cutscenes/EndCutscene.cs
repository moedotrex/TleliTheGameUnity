using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
   /* AsyncOperation sceneTleli = SceneManager.LoadSceneAsync("Tleli", LoadSceneMode.Additive);
    AsyncOperation sceneTut = SceneManager.LoadSceneAsync("Estetica_TUTORIAL", LoadSceneMode.Additive);

    public void Awake()
    {
        sceneTut.allowSceneActivation = false;
        sceneTleli.allowSceneActivation = false;
    }

    public void ChangeScene()
    {
        sceneTleli.allowSceneActivation = true;
        sceneTut.allowSceneActivation = true;
    }*/

    public string levelName = "Tleli";
    public string levelNameTut = "Estetica_TUTORIAL";
    AsyncOperation asyncTleli;
    AsyncOperation asyncTut;

    public void StartLoading()
    {
        StartCoroutine("load");
    }

    IEnumerator load()
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        asyncTleli = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        asyncTut = SceneManager.LoadSceneAsync(levelNameTut, LoadSceneMode.Additive);
        asyncTleli.allowSceneActivation = false;
        asyncTut.allowSceneActivation = false;
        yield return asyncTleli;
        yield return asyncTut;
    }

    public void ActivateScene()
    {
        asyncTleli.allowSceneActivation = true;
        asyncTut.allowSceneActivation = true;
    }

}
