using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TlelliSonido : MonoBehaviour
{
    public GameObject player;
    PlayerController controller;
  

    [FMODUnity.EventRef]
    public string inputwalksound;

    bool playerismoving;
    private bool isGrounded;
    public float walkingspeed;

    
    void Start()
    {
        
        controller = player.GetComponent<PlayerController>();
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("CallFootSteps", 0, walkingspeed);
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.isGrounded;

        if (isGrounded && Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            playerismoving = true;

        }else if (!isGrounded && Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playerismoving = false;
        }



    }

    private void CallFootSteps()
    {
        if (playerismoving && isGrounded == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputwalksound);
        }
    }

   
    private void OnDisable()
    {
        playerismoving = false;
    }
}
