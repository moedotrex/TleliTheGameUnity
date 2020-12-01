using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public float timetodestroy;
   void Update()
    {
        Destroy(gameObject, timetodestroy);
    }
}
