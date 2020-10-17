using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class NPC_TestTest : MonoBehaviour
{
    //Button talk- Para presionar el botton 
    //public GameObject buttonTalk;
    public GameObject dialogue;
    public Animator anim;
    public GameObject helpText;
    //Para la letra T
    bool talk;
    //Para rotar NPC hacia su Target
    RotNPCTest rotNPC;

    private Vector3 prevPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        talk = false;
        helpText.SetActive(false);
        dialogue.SetActive(false);
        rotNPC = GetComponent<RotNPCTest>();
        //buttonTalk.SetActive(false); 
    }

    // Update is called once per frame
   //Actualizando constantemente
    void Update()
    {
        //Para que cuando se aprete la letra T se mande de botton a Dialogue
        if (talk)
        {
            if (Input.GetKeyDown("t"))
            {
                Talk();
            }

        }

       
        
    }

    private void LateUpdate()
    {

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Talking"))
        {
            rotNPC.TurnNPC();
            rotNPC.TurnNPCHead();
        }
    }
    //Aparezca el botton     
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            talk = true;
            helpText.SetActive(true);
            //buttonTalk.SetActive(true);
            anim.SetTrigger("Stand");
            prevPosition = other.transform.position;
            
            

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !other.transform.position.Equals(prevPosition))
        {
            rotNPC.StartRotation();
            prevPosition = other.transform.position;
        }
    }
    //Para que ya no aparezcan ninguno de los dos botones ni dialogo cuando sales
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            talk = false;
            dialogue.SetActive(false);
            helpText.SetActive(false);
            //buttonTalk.SetActive(false);
            anim.SetTrigger("Sit");

        }
    }
    //Para mandar a llamar el dialogo a traves del botton
    public void Talk()
    {
        dialogue.SetActive(true);
        helpText.SetActive(false);
        //buttonTalk.SetActive(false);

    }
}
