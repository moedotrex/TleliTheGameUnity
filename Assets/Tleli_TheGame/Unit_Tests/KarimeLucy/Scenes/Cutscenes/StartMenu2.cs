using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu2 : MonoBehaviour
{
    public int firstGame;

    private void Awake()
    {
        firstGame = PlayerPrefs.GetInt("firstGame", 1);

    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Cutscene_1");

        /* if (firstGame == 1)
         {
             PlayerPrefs.SetInt("firstGame", 0);
             SceneManager.LoadSceneAsync("Cutscene_1");
         }
         else
         {
             SceneManager.LoadScene("Tleli");
             //PlayerPrefs.SetInt("firstGame", 0);
         }
          */
    }


}
