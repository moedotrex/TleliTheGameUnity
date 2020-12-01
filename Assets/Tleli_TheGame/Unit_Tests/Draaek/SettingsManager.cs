using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider camSensitivy;
    public Slider volSlider;

    public CameraSensitivity cam;

    public GameObject settingsMenuUI;
    public GameObject pauseMenuUI;

    MusicaDinamica ambient;

    float holder;
    float vol;


    // Start is called before the first frame update
    void Start()
    {
        ambient = GameObject.Find("GameManager").GetComponent<MusicaDinamica>();
        camSensitivy.value = PlayerPrefs.GetFloat("CameraSensitivity",1f);
        volSlider.value = PlayerPrefs.GetFloat("GameVolume",1f);
        
    }

    public void ReturnMainMenu()
    {
        holder = camSensitivy.value; 
        cam.SetCameraSensitivity(holder);


        vol = volSlider.value;
        ambient.Music.setVolume(vol);
        

        PlayerPrefs.SetFloat("CameraSensitivity", holder);
        PlayerPrefs.SetFloat("GameVolume", vol);

        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
