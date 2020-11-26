using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public Yarn.Unity.DialogueUI dialogueUI;
    private int currentOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            dialogueUI.MarkLineComplete();
        }
        //if (dialogueUI.onOptionsStart())

        if (Input.GetKeyDown("1"))
        {
            dialogueUI.SelectOption(0);
        }
        if (Input.GetKeyDown("2"))
        {
            dialogueUI.SelectOption(1);
        }
    }
    public void setUpOptions()
    {

    }
    

}
