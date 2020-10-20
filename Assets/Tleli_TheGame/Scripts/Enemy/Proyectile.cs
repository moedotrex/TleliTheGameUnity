using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float speed;
    private Vector3 target;
    private Transform player;

    void Start()
    {
        
       player = GameObject.FindGameObjectWithTag("Player").transform; 
       target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

   
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {

            DestroyProjectile();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Enemy"))
        {

        }
         else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("destroded1");

        }

         if (other.CompareTag("Solid"))
        {
            Destroy(gameObject);
            Debug.Log("destroded2");
        }
        Debug.Log("coliis");

        
    }
    
    void DestroyProjectile()
    {
        Destroy(gameObject);
        Debug.Log("destroded");
    }
}
