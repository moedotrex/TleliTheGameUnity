﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class OpenDoor : MonoBehaviour
{
    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    //Scene state
    private bool isLoaded;
    private bool shouldLoad;

    BossDoorAnimationController bossDoorAnimationController;

    void Start()
    {
        bossDoorAnimationController = GameObject.FindGameObjectWithTag("BossDoor").GetComponent<BossDoorAnimationController>();

        //verify if the scene is already open to avoid opening a scene twice
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (checkMethod == CheckMethod.Distance)
        {
            DistanceCheck();
        }
        else if (checkMethod == CheckMethod.Trigger)
        {
            TriggerCheck();
        }
    }

    void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }


    void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void UnLoadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    void BossKeysCheck()
    {
        if (GameEvent.llaves == 2)
        {
            bossDoorAnimationController.BossKeysTrigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
            BossKeysCheck();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }

    void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }
}
