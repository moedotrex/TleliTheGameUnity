using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrokenWall : MonoBehaviour
{
    public GameObject whatToSpawn;

    public void SpawnObject()
    {
        
     Instantiate(whatToSpawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
