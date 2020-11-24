using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreens : MonoBehaviour
{
    public int firstGame;
    public Sprite[] screens;
    Image img;

    private void Awake()
    {
        firstGame = PlayerPrefs.GetInt("firstGame", 1);
        img = gameObject.GetComponent<Image>();

        if (firstGame == 1)
        {
            img.sprite = screens[0];
        }

        else
        {
            img.sprite = screens[1];
        }
    }
}
