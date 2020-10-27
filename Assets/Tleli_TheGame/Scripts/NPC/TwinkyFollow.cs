using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TwinkyFollow : MonoBehaviour
{
    public float speed;
    public Transform TWaypoint;
    public bool Hold;
    public bool following;
    public float tleliCloseness;

    Transform target;


    void Start()
    {
        following = true;
        target = PlayerManager.instance.player.transform;

    }

    void Update()
    {

        float distance = Vector3.Distance(target.position, transform.position);


        if (distance >= tleliCloseness)
        {
            speed = 6f;
            following = true;
            Hold = false;
            Debug.Log("ikni got scared");
        }


        transform.position = Vector3.MoveTowards(transform.position, TWaypoint.position, speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tleliCloseness);
    }
}
