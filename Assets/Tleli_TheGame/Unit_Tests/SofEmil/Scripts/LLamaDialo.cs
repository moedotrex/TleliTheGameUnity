using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLamaDialo : MonoBehaviour
{
    public Yarn.Unity.DialogueRunner dialogueRunner;
    public string nodoDialogo;
    public GameObject helpText;
    bool talk;
    public GameObject cajaDialogue;
    // Start is called before the first frame update
    void Start()
    {
        helpText.SetActive(false);
        talk = false;
        
    }

    void Update()
    {
        if (talk)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                cajaDialogue.SetActive(true);
                helpText.SetActive(false);
                dialogueRunner.StartDialogue(nodoDialogo);
                
            }
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            helpText.SetActive(true);
            talk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cajaDialogue.SetActive(false);
        helpText.SetActive(false);
        talk = false;
        dialogueRunner.Stop();
    }
}
