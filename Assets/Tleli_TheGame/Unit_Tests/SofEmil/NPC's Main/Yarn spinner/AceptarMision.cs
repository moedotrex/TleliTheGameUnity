using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceptarMision : MonoBehaviour
{
    public Yarn.Unity.DialogueRunner dialogueRunner;
    public string nodoDialogo;
    CountObjects countObjects;
    public GameObject Upis;
    bool misionUno;


    private void Start()
    {
        countObjects = GetComponent<CountObjects>();
        countObjects.enabled = false;
        misionUno = false;
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Upis");
        foreach (GameObject o in collectables)
        {
            o.GetComponent<Collider>().enabled = false;

        }


    }

    [Yarn.Unity.YarnCommand("AceptarMisionUno")]
    public void AceptarMisionUno()
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Upis");
        foreach (GameObject o in collectables)
        {
            o.GetComponent<Collider>().enabled = true;

        }
        //Upis.SetActive(true);
        countObjects.enabled = true;
        Debug.Log("Mision uno Aceptada");
    }
    public void CompletaMisionUno()
    {
        misionUno = true;
        //dialogueRunner.StartDialogue(nodoDialogo);
    }

}
