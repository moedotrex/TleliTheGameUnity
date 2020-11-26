using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceptarMision : MonoBehaviour
{
    public Yarn.Unity.DialogueRunner dialogueRunner;
    public string tasiYes;
    public string nodoDialogo;
    CountObjects countObjects;
    private void Start()
    {
        countObjects = GetComponent<CountObjects>();
        countObjects.enabled = false;
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Upis");
        foreach (GameObject o in collectables)
        {
            o.GetComponent<Collider>().enabled = false;
         
        }
        if (GameObject.Find("TASI.QUEST5"))
        {
            dialogueRunner.StartDialogue(nodoDialogo);
        }

    
   

}
    [Yarn.Unity.YarnCommand("AceptarMisionUno")]
    public void AceptarMisionUno()
    {
        Debug.Log("Mision uno Aceptada");
    }

}
