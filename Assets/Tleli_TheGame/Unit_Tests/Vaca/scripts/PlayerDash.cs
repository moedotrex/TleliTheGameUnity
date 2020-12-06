using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDash : MonoBehaviour
{
    PlayerController moveScript;
    TleliDeath tleliDeath;

    public PlayerController Tleli;

    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    float dashCooldownTime;
    public bool gotDash;
    Animator animator;

    //public GameObject player;
    Vector3 moveDir;
    //private int dashValue =1; //para solo dashear una vez en el aire


    ParticleSystem dashSmear;  //---Karime

    TlelliSonido SendSound;//ADRIAN

    void Start()
    {
        moveScript = GetComponent<PlayerController>();
        tleliDeath = GetComponent<TleliDeath>(); //Stop actions when Tleli is Dead. By Emil.
        animator = GetComponentInChildren<Animator>();

        dashSmear = GameObject.Find("DashSmear").GetComponent<ParticleSystem>();  //---Karime
        SendSound = GetComponent<TlelliSonido>();//ADRIAN
    }

    void Update()
    {
        dashCooldownTime -= Time.deltaTime;
        if (gotDash == true) 
        { 
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0 && !tleliDeath.isDead)
        {
                Tleli.canMove = 20;
                animator.SetTrigger("DashPress");
                StartCoroutine(Dash());
            dashCooldownTime = dashCooldown;
                dashSmear.Emit(30);  //---Karime
                SendSound.Dash = true;//ADRIAN

            }
        }
    }

    IEnumerator Dash()
    {
        //animator.SetTrigger("Dash");
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward; //direccion tomada de player Y transform 
            moveScript.characterController.Move(moveDir * dashSpeed * Time.deltaTime);
            moveScript.isDisplaced = true;
            yield return null;
            animator.SetTrigger("Dash"); //animation loop
            moveScript.isDisplaced = false;
            moveScript.velocidad.y = -5f;
        }
    }
}
