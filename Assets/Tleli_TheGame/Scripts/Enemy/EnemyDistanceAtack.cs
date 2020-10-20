using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAtack : MonoBehaviour
{
    private float timeBtwShots;
    public float starTimeBtwShots;

    public GameObject projectile;
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = starTimeBtwShots;
            Debug.Log("fire");
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
