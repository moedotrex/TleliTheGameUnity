using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LlavesBossroom : MonoBehaviour
{
    
    public Text taskText;
    public static int llaves = 0;
    public float esperaLlave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LlaveHeavyBoi()
    {
        switch (GameEvent.llaves)
        {
            case 0:
                taskText.text = "Get the other key by defeating the Miniboss - (1/2)";
                GameEvent.llaves++;
                break;
            case 1:
                taskText.text = "Return to the settlement - (2/2)";
               GameEvent.llaves++;
                break;
        }
    }
    
}
