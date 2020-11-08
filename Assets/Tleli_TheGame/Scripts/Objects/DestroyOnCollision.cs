using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject destroyedVersion; //version destruida de la pared

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword") //para romper las paredes destructibles. lo agrego Moe
        {
            //other.gameObject.GetComponent<ParedDestructible>().Destroy
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
