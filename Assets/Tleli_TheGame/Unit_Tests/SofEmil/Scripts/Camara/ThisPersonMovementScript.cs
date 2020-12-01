using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
//Movimiento Camara: Sof y Emil

//Cinemachine plugin IS NEEDED: DESCARGAR ANTES DE SUBIR 
public class ThisPersonMovementScript : MonoBehaviour
{
    //Motor que mueve al personaje 
    public CharacterController controller;
    public Transform cam;
    
    public float speed = 6f;
    //Para que voltee smoothly
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //walk
    //TO DO: Make movement follow camera more smoothly (like a platformer, e.j. BOTW) not so sudden (like a TPS)
    //TO DO: Make Tlelli transparent when camera is down to be able to see upwards

    // Update is called once per frame
    void Update()
    {
        //Indicador de Movimiento de personaje en Horitzontal (ninguno vertical)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //If para checar que si se este movimento horizontalmente
        if(direction.magnitude >= 0.1f)
        {
            //Cuanto deberia de rotar nuestro personaje, voltear a diferentes lados
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Para que voltee smoothly
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Vector3: para que se mueve de rotation a direction 
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); 

        }

    }
}
