using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNetNew : MonoBehaviour
{
    //Jules - Same as old code, but remade cause there's something using the old code, maybe
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
        }
    }
}
