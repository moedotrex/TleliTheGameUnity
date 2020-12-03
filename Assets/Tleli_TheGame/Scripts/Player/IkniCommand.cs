using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.WSA;

public class IkniCommand : MonoBehaviour
{

    TwinkyFollow ikniFollow;
    IkniSlowAction ikniSlowAOE;
    TleliDeath tleliDeath;
    public float timeforCommand;
    public float slowCD;
    float inSlowCD;
    float commandTimer;
    bool commandReceived;
    public bool whichAction;
    float timeHeld;

    void Start()
    {
        inSlowCD = 0f;
        ikniFollow = GameObject.FindGameObjectWithTag("Ikni").GetComponent<TwinkyFollow>();
        ikniSlowAOE = GameObject.FindGameObjectWithTag("Ikni").GetComponent<IkniSlowAction>();
        tleliDeath = GetComponent<TleliDeath>(); //Stop actions when Tleli is Dead. By Emil.
    }


    void Update()
    {
        inSlowCD -= Time.deltaTime;

        if (Input.GetKey("q"))
        {
            timeHeld += Time.deltaTime;

            if (timeHeld >= timeforCommand && commandReceived == false)
            {
                Debug.Log("long press");
                ikniHold();
            }
        }

        if (Input.GetKeyUp("q"))
        {
            if (timeHeld <= timeforCommand && inSlowCD <= 0f && !tleliDeath.isDead)
            {
                Debug.Log("shortpress");
                ikniSlowAOE.SlowInk();
                inSlowCD = slowCD;
            }

            timeHeld = 0f;
            commandReceived = false;
        }
    }

    public void ikniHold()
    {
        if (ikniFollow.following == true)
        {
            ikniFollow.speed = 0f;
            ikniFollow.following = false;
            ikniFollow.Hold = true;
            commandTimer = 0f;
            commandReceived = true;
        }
        else if (ikniFollow.following == false)
        {
            ikniFollow.speed = 6f;
            ikniFollow.following = true;
            ikniFollow.Hold = false;
            commandTimer = 0f;
            commandReceived = true;
        }
    }
}
