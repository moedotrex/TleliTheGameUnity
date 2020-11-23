using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterStaticEnemy : MonoBehaviour
{
    public float posX;
    public float posZ;
    public bool atackDistance = true;

    public float timeTransport;
    public void transport()
    {
        atackDistance = false;
             stopMov();
            
           
    }

    public void stopMov()
    {
        StartCoroutine(stopMovCoroutine());
    }

    IEnumerator stopMovCoroutine()
    {

        yield return new WaitForSeconds(timeTransport);
        transform.position = new Vector3(Random.Range(-posX, posX), transform.position.y, Random.Range(-posZ, posZ));
        atackDistance = true;
    }
}
