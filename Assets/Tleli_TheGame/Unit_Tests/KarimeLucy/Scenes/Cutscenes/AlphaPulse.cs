using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaPulse : MonoBehaviour
{
    float a;
    Color color;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        color = txt.color;
    }

    // Update is called once per frame
    void Update()
    {
        //a = Mathf.PingPong(Time.time, 1f);
        float lerp = Mathf.PingPong(Time.time, 1.5f) / 1.5f;
        a = Mathf.Lerp(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, lerp));
        txt.color = new Color(1, 1, 1, a);
    }
}
