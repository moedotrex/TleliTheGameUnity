using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool GameIsPaused;
    public string screen;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public LightCombo lightAttack;
    public HeavyCombo heavyAttack;
    public PlayerController player;

    MusicaDinamica ambient;


    void Start()
    {
        lightAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<LightCombo>();
        heavyAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<HeavyCombo>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ambient = GameObject.Find("GameManager").GetComponent<MusicaDinamica>();


    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && screen == "pause")
            {
                Resume();
            }
            else if (GameIsPaused && screen == "settings")
            {
                ReturnToPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //------------------ ENABLING SCRIPTS ------------------\\
        lightAttack.enabled = true;
        heavyAttack.enabled = true;
        player.enabled = true;

        //------------------ RESUME ------------------\\
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Pause()
    {

        //------------------ DISABLING SCRIPTS ------------------\\
        lightAttack.enabled = false;
        heavyAttack.enabled = false;
        player.enabled = false;

        //------------------ PAUSE ------------------\\
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        screen = "pause";
    }

    public void Restart()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //ambient.Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene("MainTest_Scene");
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        screen = "settings";
    }

    void ReturnToPause()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        screen = "pause";
    }
}
