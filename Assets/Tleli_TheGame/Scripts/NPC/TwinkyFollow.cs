using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TwinkyFollow : MonoBehaviour
{
    public float speed;
    public Transform TWaypoint;

    void Start()
    {
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TWaypoint.position, speed * Time.deltaTime);
    }
}
