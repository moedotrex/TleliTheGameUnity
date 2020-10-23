using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC File", menuName = "NPC Files Archive")]
public class NPC_Eric_Test : ScriptableObject
{
    //Su nombre
    public string name;
    [TextArea(3,15)]
    //Sus lineas que va a decir
    public string [] dialogue;
    [TextArea(3, 15)]
    //Possibles respuestas del jugador
    public string[] playerDialogue;
 
}
