using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hue : MonoBehaviour
{
    public Material CambioDeColor;

    public float hue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hue = Mathf.Lerp(hue, 0, Time.deltaTime);

        CambioDeColor.SetFloat("_hue", hue);

        if (Input.GetButtonDown ("Jump"))
        {
            hue += 1f;
        }
    }
}
