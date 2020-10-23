using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_manager :MonoBehaviour
{
    public NPC_Eric_Test npc;
    bool isTalking = false;
    float distance;
    float curResponseTracker = 0;
    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;


   
   
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }
    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance <= 2.5f)
        {
            //Trigger dialogue
            if(Input.GetKeyDown(KeyCode.E) && isTalking==false)
            {
                StartConversation();

            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }
        
        }
 
    }
    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }
    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }

}
