using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public Yarn.Unity.DialogueUI dialogueUI;
    private int currentOption = 0;
    private int numOptions;
    public List<UnityEngine.UI.Button> options;


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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentOption == 1)
            {
                currentOption = numOptions;
            }
            else
            {
                currentOption--;

            }
            Debug.Log(currentOption);
            options[currentOption - 1].Select();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOption == numOptions)
            {
                currentOption = 1;
            }
            else
            {
                currentOption++;
                
            }
            Debug.Log(currentOption);
            options[currentOption - 1].Select();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {

        }
    }
    public void setUpOptions()
    {
        Debug.Log(currentOption);
        currentOption = 1;
        options[0].Select();
        options[0].OnSelect(null);
        if (options[3].gameObject.activeSelf)
        {
            numOptions = 4;
        }
        else
        {
            if (options[2].gameObject.activeSelf)
            {
                numOptions = 3;
            }
            else
            {
                numOptions = 2;
            }
        }
    }


}
