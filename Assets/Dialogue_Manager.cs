using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue_Manager : MonoBehaviour
{
    //Su nombre
    public string npcName;
    [TextArea(3, 15)]
    //Lineas que va a decir el NPC
    public string[] npcDialogue;
    [TextArea(3, 15)]
    //Possibles respuestas del jugador
    public string[] playerDialogue;
    //Button talk- Para presionar el botton 
    //public GameObject buttonTalk;
    public GameObject helpText;
    //NO ESCALA NECESARIA, multiplica al radio del colider, podria ser uno con modelos corretamente escalados
    public float radiusFactor = 70.0f;
    //Para la letra T
    bool talk;
    Animator anim;

    float distance;
    float minDistance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    public Text npcShow;
    public Text npcDialogueBox;
    public Text playerResponse;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        minDistance = radiusFactor * capsuleCollider.radius;
        talk = false;
        helpText.SetActive(false);
        dialogueUI.SetActive(false);
        npcShow.text = npcName;
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= minDistance)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if (curResponseTracker >=playerDialogue.Length - 1)
                {
                    curResponseTracker =playerDialogue.Length - 1;
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }
            //trigger dialogue
            if (Input.GetKeyDown(KeyCode.T) && talk == false)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.T) && talk == true)
            {
                EndDialogue();
            }

            if (curResponseTracker == 0 && playerDialogue.Length >= 0)
            {
                playerResponse.text = playerDialogue[0];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npcDialogue[1];
                }
            }
            else if (curResponseTracker == 1 && playerDialogue.Length >= 1)
            {
                playerResponse.text = playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npcDialogue[2];
                }
            }
            else if (curResponseTracker == 2 && playerDialogue.Length >= 2)
            {
                playerResponse.text = playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npcDialogue[3];
                }
            }

        }
    }

    void StartConversation()
    {
        talk = true;
        curResponseTracker = 0;
        //dialogueUI.SetActive(true);
        npcShow.text = npcName;
        npcDialogueBox.text = npcDialogue[0];
    }

    void EndDialogue()
    {
        talk = false;
        //dialogueUI.SetActive(false);
    }
    // Update is called once per frame
    //Actualizando constantemente
    void Update()
    {
        //Para que cuando se aprete la letra T se mande de botton a Dialogue
        if (talk)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Talk();
            }

        }

    }
    //Aparezca el botton     
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            helpText.SetActive(true);
            //buttonTalk.SetActive(true);
            anim.SetTrigger("Stand");
            talk = true;

        }
    }
    //Para que ya no aparezcan ninguno de los dos botones ni dialogo cuando sales
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            talk = false;
            helpText.SetActive(false);
            //buttonTalk.SetActive(false);
            anim.SetTrigger("Sit");
            dialogueUI.SetActive(false);

        }
    }
    //Para mandar a llamar el dialogo a traves del botton
    public void Talk()
    {
        curResponseTracker = 0;
        helpText.SetActive(false);
        dialogueUI.SetActive(true);
        playerResponse.text = playerDialogue[0];
        npcDialogueBox.text = npcDialogue[0];
        //buttonTalk.SetActive(false);

    }

}

