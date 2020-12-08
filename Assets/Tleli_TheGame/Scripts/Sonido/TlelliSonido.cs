using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TlelliSonido : MonoBehaviour
{
    public GameObject player;
    PlayerController controller;

    private FMOD.Studio.EventInstance GroundTypeRef; //ADRIAN: al parecer no puedes manipular variables de FMOD sin hacer una instancia de evento
    private FMOD.Studio.EventInstance JumpTypeRef;
    private FMOD.Studio.EventInstance LAChargeInst;

    [FMODUnity.EventRef]
    public string inputwalksound;


    bool playerismoving;

    //ADRIAN
    [HideInInspector] public bool FlameIsDamaged;
    [HideInInspector] public bool FlameIsDepleted = false;
    [HideInInspector] public bool FlameIsFull = false;
    [HideInInspector] public bool LAttack;
    [HideInInspector] public bool LACharge;
    [HideInInspector] public bool LAChargeLock;
    [HideInInspector] public bool Dash;
    [HideInInspector] public bool TleliIsDead;
    [HideInInspector] public bool TleliResurrects;
    [HideInInspector] public bool GroundSound;
    [HideInInspector] private bool GroundSoundLock;
    [HideInInspector] private bool isGrounded;
    public float walkingspeed;
    private int GroundType;
    //
    void Start()
    {
        GroundTypeRef = FMODUnity.RuntimeManager.CreateInstance("event:/Land");
        JumpTypeRef = FMODUnity.RuntimeManager.CreateInstance("event:/TleliStuff/TleliJump");
        LAChargeInst = FMODUnity.RuntimeManager.CreateInstance("event:/TleliStuff/TleliLACharge");

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
        CallLACharge();
        CallDepletion();
        CallFulFillment();
        CallDash();
        HitsGround();
        Jump();
        //DoubleJump();
        TleliDies();
        TleliResurrected();
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

    private void CallLACharge()
    {
        if (LACharge)
        {
         
            if (!LAChargeLock)
            {
                LAChargeInst.start();
                LAChargeLock = true;
            }

        }
        else
        {
            LAChargeInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            LACharge = false;
            LAChargeLock = false;
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
        JumpTypeRef.setParameterByName("Jump_Variation", GroundType);
        GroundType = 3;
        //ADRIAN
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            JumpTypeRef.start();
        }
        
        //
    }

    public void DoubleJump()
    {
        /*if (Input.GetButtonDown("Jump") && controller.extraJumps >= 0 && !controller.isGrounded && controller.isJumping)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliDoubleJump");
            
        }*/
        FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliDoubleJump");
    }

    private void TleliDies()
    {
        if(TleliIsDead)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliDeath");
            TleliIsDead = false;
        }
    }

    private void TleliResurrected()
    {
        if (TleliResurrects)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/TleliStuff/TleliResurrects");
            TleliResurrects = false;
        }
    }
    

    private void OnDisable()
    {
        playerismoving = false;
    }
}
