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


    public GameObject alertIcon;
    public bool alertActive;
    public float platCloseness;

    Transform target;

    AlertChange changeAlert;
    float actualDistanceToPlat;

    void Start()
    {
        
        following = true;
        target = PlayerManager.instance.player.transform;
        changeAlert = GetComponentInChildren<AlertChange>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {

       float distance = Vector3.Distance(target.position, transform.position);


        if (distance >= tleliCloseness)
        {
            speed = 6f;
            following = true;
            Hold = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, TWaypoint.position, speed * Time.deltaTime);
    }

    void UpdateTarget()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("InvisPlatform");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlat = null;
        
        foreach (GameObject plat in platforms)
        {
            float distanceToPlat = Vector3.Distance(transform.position, plat.transform.position);
            if (distanceToPlat < shortestDistance)
            {
                shortestDistance = distanceToPlat;
                nearestPlat = plat;
                changeAlert.platDistance(distanceToPlat);
            }
           // giveMeTheDistance(distanceToPlat);
        }

        if (nearestPlat != null && shortestDistance <= platCloseness)
        {
                alertIcon.SetActive(true);
                alertActive = true;
            
           
            // target = nearestPlat.transform;
        }

        else
        {
            //target = null;
            alertIcon.SetActive(false);
            alertActive = false;
        }
    }

    void giveMeTheDistance(float dist)
    {
        actualDistanceToPlat = dist;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tleliCloseness);
    }


}
