using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LLamaDialo : MonoBehaviour
{
    public Yarn.Unity.DialogueRunner dialogueRunner;
    public string nodoDialogo;
    public GameObject helpText;
    bool talk;
    public GameObject cajaDialogue;
    CinemachineFreeLook camm;

    
    // Start is called before the first frame update
    void Start()
    {
        helpText.SetActive(false);
        talk = false;
        //camm = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (talk)
        {
            if (Input.GetKeyDown(KeyCode.F)&& !dialogueRunner.IsDialogueRunning)
            {
                
                cajaDialogue.SetActive(true);
                helpText.SetActive(false);
                dialogueRunner.StartDialogue(nodoDialogo);
                //camm.m_YAxis.m_MaxSpeed = 0;
                //camm.m_XAxis.m_MaxSpeed = 0;
            

            }
        else if (!dialogueRunner.IsDialogueRunning)
            {
                //camm.m_YAxis.m_MaxSpeed = 1.5f;
                //camm.m_XAxis.m_MaxSpeed = 350;
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
