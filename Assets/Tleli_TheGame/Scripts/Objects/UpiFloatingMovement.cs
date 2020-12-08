using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpiFloatingMovement : MonoBehaviour
{
 //Rango y frecuencia con la que ocurren los "rebotes" al flotar.
    float bounceAmplitude = 0.12f;
    float bounceFrequency = .7f;
    Vector3 OffsetPosition = new Vector3();
    Vector3 TempPosition = new Vector3();
  
    void Start()
    {
        OffsetPosition = transform.position;
    }

   
    void Update()
    {
        //Utilizé una sinewave que afecta el vector Y para un movimiento vertical de arriba a abajo
        TempPosition = OffsetPosition;
        TempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * bounceFrequency) * bounceAmplitude;
        transform.position = TempPosition;
    }
}
