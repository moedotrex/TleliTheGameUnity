using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterStaticEnemy : MonoBehaviour
{
    public float posX;
    public float posZ;

    public float timeTransport;
    public void transport()
    {
             stopMov();
            
            //timeTransport = 10f;
        //}
    }

    public void stopMov()
    {
        StartCoroutine(stopMovCoroutine());
    }

    IEnumerator stopMovCoroutine()
    {
        
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(Random.Range(-posX, posX), transform.position.y, Random.Range(-posZ, posZ));

    }
}
