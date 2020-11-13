using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkniSlowAction : MonoBehaviour
{
    public GameObject inkSack;
   // public GameObject inkParticle;
    Ray ray;
    RaycastHit hit;
    //public LayerMask mask;
    //public float range;

    public void SlowInk()
    {
        Instantiate(inkSack,transform.position, Quaternion.identity);
       /* ray.origin = transform.position;
        ray.direction = -transform.up;
        Debug.DrawRay(transform.position, -transform.up, Color.red);

        if (Physics.Raycast(ray, out hit, range, mask))
        {
                Debug.Log("hit ground");
                Instantiate(inkSack, hit.point, Quaternion.identity);
        }*/
    }

}
