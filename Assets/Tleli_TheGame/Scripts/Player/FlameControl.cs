using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Karime
// Controlar intensidad de la flama en el shader

public class FlameControl: MonoBehaviour
{
   
    public Material matFlama;
    //TlelliFlameHealth tlelli;
    TleliHealth tlelli;
    float flama;

    
    void Start()
    {
        
       // tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();
        flama = tlelli.GetFlameIntensity();
        matFlama.SetFloat("_FLAMELEVEL", flama);
    }

    
    void FixedUpdate()
    {
        flama = tlelli.GetFlameIntensity();       
        matFlama.SetFloat("_FLAMELEVEL", flama);

    }
}
