using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableMMap : MonoBehaviour
{
    //Image minimap;
    //Image marcador;
    Vector3 TleliYPosition;
    public GameObject MapToShow;
    public GameObject UpperMap;
    public GameObject LowerMap;

    // Start is called before the first frame update
    void Start()
    {
        TleliYPosition = transform.position;
        //minimap = GameObject.Find("Mapa_0").GetComponent<Image>();
        //minimap.enabled = false;
        //marcador = GameObject.Find("Marcador").GetComponent<Image>();
        //marcador.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TleliYPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.M))
        {
            //minimap.enabled = !minimap.enabled;
            //marcador.enabled = !marcador.enabled;
            MapToShow.SetActive(true);
        }
        if (TleliYPosition.y < 0f)
        {
            Debug.Log("Tleli esta abajo");
            MapToShow = LowerMap;
            UpperMap.SetActive(false);
            MapToShow.SetActive(true);
        }
        else
        {
            Debug.Log("Tleli esta arriba");
            MapToShow = UpperMap;
            LowerMap.SetActive(false);
            MapToShow.SetActive(true);
        }
    }

}
