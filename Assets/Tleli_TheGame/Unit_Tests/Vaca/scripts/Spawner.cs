using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timeToSpawn;
    public GameObject objectToSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn <= 0)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
                
        }
    }
}
