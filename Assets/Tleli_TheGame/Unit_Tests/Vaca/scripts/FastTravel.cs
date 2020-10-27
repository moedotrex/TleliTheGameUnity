using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{

    public GameObject helpText;
    public GameObject map;
    bool talk;
    public Transform MainDestination;
    public Transform IslandDestination;
    GameObject player;



    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    void Update()
    {
        if (talk)
        {
            if (Input.GetKeyDown("f"))
            {
                Map();
                Time.timeScale = 0f;
            }
       
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                map.SetActive(false);
            }
        }

      
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FastTravel")
        {
            talk = true;
            helpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FastTravel")
        {
            talk = false;
            map.SetActive(false);
            helpText.SetActive(false);
        }
    }

    public void Map()
    {
        map.SetActive(true);
        helpText.SetActive(false);
    }

    public void tpIsland()
    {
        map.SetActive(false);
        Time.timeScale = 1f;
        transform.position = IslandDestination.position;
    }

    public void tpMain()
    {
        map.SetActive(false);
        Time.timeScale = 1f;
        transform.position = MainDestination.position;
    }
}
