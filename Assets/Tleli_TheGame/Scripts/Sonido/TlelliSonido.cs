using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TlelliSonido : MonoBehaviour
{
    public GameObject player;
    PlayerController controller;

    private FMOD.Studio.EventInstance GroundTypeRef; //ADRIAN: al parecer no puedes manipular variables de FMOD sin hacer una instancia de evento
    

    [FMODUnity.EventRef]
    public string inputwalksound;


    bool playerismoving;
    
    //ADRIAN
    public bool FlameIsDamaged; 
    public bool FlameIsDepleted = false;
    public bool FlameIsFull = false;
    public bool LAttack;
    public bool Dash;
    public bool GroundSound;
    private bool GroundSoundLock;
    private bool isGrounded;
    public float walkingspeed;
    private int GroundType;
    //
    void Start()
    {
        GroundTypeRef= FMODUnity.RuntimeManager.CreateInstance("event:/Land");

        controller = player.GetComponent<PlayerController>();
        player = GameObject.FindWithTag("Player");
        
        InvokeRepeating("CallFootSteps", 0, walkingspeed);
       
}

    // Update is called once per frame
    void Update()
    {
        //ADRIAN
        CallHit(); 
        CallAttack();
        CallDepletion();
        CallFulFillment();
        CallDash();
        HitsGround();
        //

        //print("L is " + LAttack);

        isGrounded = controller.isGrounded;

        if (isGrounded && Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            playerismoving = true;

        }else if (!isGrounded && Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playerismoving = false;
        }

    }

    //ADRIAN metodo para reproducir sonido de perdida de flama
    private void CallFootSteps()
    {
        if (playerismoving && isGrounded == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputwalksound);
        }
    }
    //

    public void CallHit()
    {
        if(FlameIsDamaged)
        {
           
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliIsHurt");

            FlameIsDamaged = false;

        }

    }

   private void CallAttack()
    {
        if (LAttack)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/LAttackSwings");
            LAttack = false;
        }
    }

    //ADRIAN
    private void CallDepletion() 
    {
        if (FlameIsDepleted)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliFlamaVacia");
            FlameIsDepleted = false;
        }
    }
    
    private void CallFulFillment()
    {
        if (FlameIsFull)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliFlamaLlena");
            FlameIsFull = false;
        }
    }

    private void CallDash()
    {
        if(Dash)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliDash");
            Dash = false;
        }
    }

    private void HitsGround()
    {
        GroundTypeRef.setParameterByName("Land", GroundType);
        GroundType = 3;
        if (isGrounded)
        {
            if (!GroundSoundLock)
            {
                GroundTypeRef.start(); //en vez de usar el runtime manager, solo se debe invocar con la ejecucion de la instancia
                GroundSoundLock = true;
            }
        }else if (!isGrounded)
        {
            GroundSoundLock = false;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliDash");
        }
    }
    //

    private void OnDisable()
    {
        playerismoving = false;
    }
}
