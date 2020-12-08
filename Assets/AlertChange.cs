using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertChange : MonoBehaviour
{
    //public Renderer rend;
    Animator myAnim;

    private void Start()
    {
        //  GetComponent<Material>();
        myAnim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (Input.GetKey("x"))
        {
         //  GetComponent<Renderer>().material.color = Color.red;
           // GetComponent<Renderer>().material.color = Color.Lerp(Color.blue , Color.red, 2f);
        }
    }



    public void platDistance(float d)
    {
        // GetComponentInChildren<Renderer>().material.color = Color.red;
        float dist = d;
       // dam = (1 / dist) * flameDamage;
       // SetFlameDamage(dam);

        print("Distancia: " + dist);

        if (dist > 5f )
        {
            GetComponentInChildren<Renderer>().material.color = Color.cyan;
            myAnim.speed = 1;
        }

        if (dist <= 5f )
        {
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
            myAnim.speed = 2;
        }

        if (dist <= 2.5f)
        {
            GetComponentInChildren<Renderer>().material.color = Color.red;
            myAnim.speed = 3;
        }
    }
}
