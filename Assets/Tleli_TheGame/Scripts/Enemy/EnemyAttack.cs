using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;
    public float attackDamageSlam = 1;
    public float slamCounter = 0;
    public float speed; 

    private Vector3 slamLand;
    private Transform playerPos;

    GameObject player;
    TlelliFlameHealth TlelliHealth;
    
    bool playerInRange;
    float timer;


    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        slamLand = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && slamCounter <=1)
        {
            Attack();
            slamCounter++;
        }

        if (timer >= timeBetweenAttacks && playerInRange && slamCounter >= 2)
        {
            AttackSlam();
            slamCounter = 0;
        }
        

        if (timer >= timeBetweenAttacks && slamCounter <= 1 )
        {

            JumpSlamp();
        }

    }


    void Attack()
    {
        timer = 0f;

        if (TlelliHealth.HP > 0)
        {
            TlelliHealth.SetHPDamage(attackDamage);
        }
    }

    void AttackSlam()
    {
        timer = 0f;

        if (TlelliHealth.HP > 0)
        {
            TlelliHealth.SetHPDamage(attackDamageSlam);
        }
    }

    void JumpSlamp()
    {
        timer = 0f;
        int randomNum = Random.Range(1, 3);
        if (TlelliHealth.HP > 0 && randomNum ==2)
        {
            transform.position = Vector3.MoveTowards(transform.position, slamLand, speed * Time.deltaTime);
            if(playerInRange)
            {
                TlelliHealth.SetHPDamage(attackDamageSlam);
            }
        }
    }

    //¿jugador en rango de ataque?
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    //jugador fuera de rango
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

}
