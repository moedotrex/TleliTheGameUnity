using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject pauseMenuUI;
    public LightCombo lightAttack;
    public HeavyCombo heavyAttack;
    public PlayerController player;

    void Start()
    {
        lightAttack = GameObject.Find("Player").GetComponent<LightCombo>();
        heavyAttack = GameObject.Find("Player").GetComponent<HeavyCombo>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
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
    }

    public void Restart()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainTest_Scene");
    }
}
