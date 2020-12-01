using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Values : MonoBehaviour
{
    public Text text;
    public Slider inputValue;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = inputValue.value.ToString("F2");
    }
}
