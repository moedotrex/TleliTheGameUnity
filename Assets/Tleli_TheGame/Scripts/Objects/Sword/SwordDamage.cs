using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public GameObject destroyedVersion; //version destruida de la pared

    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(Damage);
        }

        if (other.gameObject.tag == "Destructible") //para romper las paredes destructibles. lo agrego Moe
        {
            //other.gameObject.GetComponent<ParedDestructible>().Destroy
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
