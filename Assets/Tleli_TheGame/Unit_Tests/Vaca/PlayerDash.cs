using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDash : MonoBehaviour
{
    PlayerController moveScript;

    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    float dashCooldownTime;
    public bool gotDash;
    //private int dashValue =1; //para solo dashear una vez en el aire


    void Start()
    {
        moveScript = GetComponent<PlayerController>();
    }

    void Update()
    {
        dashCooldownTime -= Time.deltaTime;
        if (gotDash == true) 
        { 
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0)
        {
            StartCoroutine(Dash());
            dashCooldownTime = dashCooldown;
        }
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveScript.characterController.Move(moveScript.moveDir * dashSpeed * Time.deltaTime); //direccion tomada de playercontroller
            moveScript.isDashing = true; 
            yield return null; 
            moveScript.isDashing = false;
        }
    }
}
