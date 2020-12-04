using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public Vector3 pushSpeed= new Vector3(1,0,1);
    private void OnTriggerStay(Collider other)
    {
        other.transform.Translate(pushSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.Translate(pushSpeed * Time.deltaTime);
    }
}
