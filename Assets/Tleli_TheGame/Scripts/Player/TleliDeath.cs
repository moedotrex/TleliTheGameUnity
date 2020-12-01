using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TleliDeath : MonoBehaviour
{
    public Transform[] checkpoints;
    public float defaultRespawnTime=5;
    float respawnTime;
    public bool isDead=false;
    
    Transform playerTransform;
    TleliHealth tleliHealth;
    TleliAnimationController tleliAnimation;

    // Start is called before the first frame update
    void Start()
    {
        tleliAnimation = GetComponentInChildren<TleliAnimationController>();
        playerTransform = GetComponent<Transform>();
        tleliHealth = GetComponent<TleliHealth>();
        respawnTime = defaultRespawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (tleliHealth.HP <= 0)
        {
           
            isDead = true;
            tleliAnimation.IsDeadBool(isDead);
            respawnTime -= Time.deltaTime;
            if (respawnTime <= 0)
            {
                Transform newPlayerTransform = NearestCheckpoint(playerTransform);
                playerTransform.position = newPlayerTransform.position;
                tleliHealth.HP = tleliHealth.maxHP;
                tleliHealth.flame = tleliHealth.maxFlame;
                isDead = false;
                tleliAnimation.IsDeadBool(isDead);
                respawnTime = defaultRespawnTime;
            }
        }
    }


    Transform NearestCheckpoint(Transform oldPlayerTransform)
    {
        int nearest = 0;
        for(int i=0; i < checkpoints.Length; i++)
            {
            Vector3 currentNearest = new Vector3(checkpoints[nearest].position.x, checkpoints[nearest].position.y, checkpoints[nearest].position.z);
            Vector3 test = new Vector3(checkpoints[i].position.x, checkpoints[i].position.y, checkpoints[i].position.z);
            float distCurrentNearest = Mathf.Abs(Vector3.Distance(oldPlayerTransform.position, currentNearest));
            float distTest = Mathf.Abs(Vector3.Distance(oldPlayerTransform.position, test));

            if (distTest <= distCurrentNearest)
            {
                nearest = i;
            }

        }

        return checkpoints[nearest];
    }
    }

