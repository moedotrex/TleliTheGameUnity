using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableMMap : MonoBehaviour
{
    Image minimap;
    Image marcador;
    // Start is called before the first frame update
    void Start()
    {
        minimap = GameObject.Find("Mapa_0").GetComponent<Image>();
        minimap.enabled = false;
        marcador = GameObject.Find("Marcador").GetComponent<Image>();
        marcador.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            minimap.enabled = !minimap.enabled;
            marcador.enabled = !marcador.enabled;
        }
    }
}
