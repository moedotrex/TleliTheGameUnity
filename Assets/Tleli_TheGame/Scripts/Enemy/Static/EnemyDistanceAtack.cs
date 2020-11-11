using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAtack : MonoBehaviour
{
    private float timeBtwShots;
    public float starTimeBtwShots;
    public Transform shootSpot;
    public float BuscarRadio;
    Transform target;
    TransporterStaticEnemy atackStop;

    public GameObject projectile;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        atackStop = GetComponent<TransporterStaticEnemy>();
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= BuscarRadio)
        {
            if (atackStop.atackDistance == true) 
            {
                Fire();
            }
            
        }

       
       

    }
    public void Fire()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, shootSpot.position, Quaternion.identity);
            timeBtwShots = starTimeBtwShots;
            Debug.Log("fire");
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
