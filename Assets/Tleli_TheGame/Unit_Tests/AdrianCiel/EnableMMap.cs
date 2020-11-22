using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableMMap : MonoBehaviour
{
    RawImage minimap;
    // Start is called before the first frame update
    void Start()
    {
        minimap = GameObject.Find("Mapa 0").GetComponent<RawImage>();
        minimap.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            minimap.enabled = !minimap.enabled;
        }
    }
}
