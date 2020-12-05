using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public Vector3 pushSpeed= new Vector3(4,0,4);
    private void OnTriggerStay(Collider other)
    {
        other.transform.Translate(pushSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.Translate(pushSpeed * Time.deltaTime);
    }
}
