using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableMMap : MonoBehaviour
{
    //Image marcador;
    Vector3 TleliYPosition;
    public GameObject MapToShow;
    public GameObject UpperMap;
    public GameObject LowerMap;
    bool MapaPrendido;
    //Image UpMap;
    //Image DownMap;

    // Start is called before the first frame update
    void Start()
    {
        TleliYPosition = transform.position;
        MapToShow.SetActive(false);
        /*
        marcador = GameObject.Find("Marcador").GetComponent<Image>();
        marcador.enabled = false;
        UpMap = GameObject.Find("Mapa_0").GetComponent<Image>();
        UpMap.enabled = false;
        DownMap = GameObject.Find("Mapa_-1").GetComponent<Image>();
        DownMap.enabled = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
        TleliYPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.M))
        {
            MapaPrendido = !MapaPrendido;
            if (MapaPrendido == true)
            {
                MapToShow.SetActive(true);
            }
            else
            {
                MapToShow.SetActive(false);
            }

        }
        if (TleliYPosition.y < 0f)
        {
            Debug.Log("Tleli esta abajo");
            MapToShow = LowerMap;
            UpperMap.SetActive(false);
            //MapToShow.SetActive(true);
        }
        else
        {
            //Debug.Log("Tleli esta arriba");
            MapToShow = UpperMap;
            LowerMap.SetActive(false);
            //MapToShow.SetActive(true);
        }
    }

}
