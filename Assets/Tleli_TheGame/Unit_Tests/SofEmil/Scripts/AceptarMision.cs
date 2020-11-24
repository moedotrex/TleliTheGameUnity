using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceptarMision : MonoBehaviour
{
    [Yarn.Unity.YarnCommand("AceptarMisionUno")]
    public void AceptarMisionUno()
    {
        Debug.Log("Mision uno Aceptada");
    }
    [Yarn.Unity.YarnCommand("AceptarMisionDos")]
    public void AceptarMisionDos()
    {
        Debug.Log("Mision dos Aceptada");
    }

}
