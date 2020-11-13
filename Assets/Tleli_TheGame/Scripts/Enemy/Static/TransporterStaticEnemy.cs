using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterStaticEnemy : MonoBehaviour
{
    public float posX;
    public float posZ;
<<<<<<< HEAD
    public bool atackDistance = true;
=======
>>>>>>> Tech_Vaca

    public float timeTransport;
    public void transport()
    {
<<<<<<< HEAD
        atackDistance = false;
             stopMov();
            
           
=======
             stopMov();
            
            //timeTransport = 10f;
        //}
>>>>>>> Tech_Vaca
    }

    public void stopMov()
    {
        StartCoroutine(stopMovCoroutine());
    }

    IEnumerator stopMovCoroutine()
    {
<<<<<<< HEAD

        yield return new WaitForSeconds(timeTransport);
        transform.position = new Vector3(Random.Range(-posX, posX), transform.position.y, Random.Range(-posZ, posZ));
        atackDistance = true;
=======
        
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(Random.Range(-posX, posX), transform.position.y, Random.Range(-posZ, posZ));

>>>>>>> Tech_Vaca
    }
}
