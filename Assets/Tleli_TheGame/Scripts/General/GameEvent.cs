using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEvent : MonoBehaviour
{
    public string eventName;
    public Text taskText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (this.eventName)
            {
                case "FindIkni":

                    taskText.text = "Go to the cave and find Ikni";
                    break;
                case "GetDash":
                    taskText.text = "Find the cave's end with Ikni";
                    break;
                case "ReachHub":
                    taskText.text = "Jump and reach the settlement";
                    break;
                case "SpearRoom":
                    taskText.text = "Reach the weapon on top of the column";
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (this.eventName)
            {
                case "GetKeys":

                    taskText.text = "Go to the temple ruins past the mushrooms";
                    break;
            }
        }
    }
}
