using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;
    public float attackDamageSlam = 1;
    public float slamCounter = 0;
    public float speed;
    public float jumpSpeed;
    

    private Vector3 slamLand;
    private Transform playerPos;

    GameObject player;
    //TlelliFlameHealth TlelliHealth;
    TleliHealth TlelliHealth;


    tleliKnockBack playerKnockback;

    HeavyBoiAnimationController heavyBoiAnimationController; //moe 

    bool playerInRange;
    float timer;
    public bool isDisplaced;
    //crear evento para detectar tiempo de anim gethit y death

    void Start()
    {
        heavyBoiAnimationController = GetComponentInChildren<HeavyBoiAnimationController>(); //moe

        player = GameObject.FindGameObjectWithTag("Player");
        // TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        TlelliHealth = player.GetComponent<TleliHealth>();

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
       

        playerKnockback = player.GetComponent<tleliKnockBack>();

    }


    void Update()
    {
        timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && isDisplaced == false )

            {
            int randomNum = Random.Range(1, 100);
            slamCounter++;
            if (randomNum <= 66)
            {
                Attack();
            }

            if (randomNum > 66)
            {
                AttackSlam();

            }
        }

        if (timer >= timeBetweenAttacks && slamCounter >= 3)
        {

            JumpSlamp();
            slamCounter = 0; 
        }

    }


    void Attack()
    {
        timer = 0f;

        if (TlelliHealth.HP > 0)
        {
            heavyBoiAnimationController.LightAttackTrigger(); //moe

            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack(5f);
        }

        if (TlelliHealth.flame <= 0)
        {
            TlelliHealth.SetHPDamage(1);
        }
        /* if (TlelliHealth.HP < 0)
         {
        Poner una barrera para que deje de atacar
         }*/
    }

    void AttackSlam()
    {
        timer = 0f;

        if (TlelliHealth.HP > 0)
        {
            heavyBoiAnimationController.LightAttackTrigger(); //moe

            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack(5f);
        }

        if (TlelliHealth.flame <= 0)
        {
            TlelliHealth.SetHPDamage(1);
        }
    }

    void JumpSlamp()
    {
        timer = 0f;

        int randomNum = Random.Range(1, 100);
        if (TlelliHealth.HP > 0 && randomNum >= 80)
        {
            slamLand = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
            transform.position = Vector3.MoveTowards(transform.position, slamLand, jumpSpeed);

            if (playerInRange)
            {
                heavyBoiAnimationController.JumpTrigger(); //moe
                TlelliHealth.HurtFlame(attackDamage);
                playerKnockback.startKnockBack(5f);
            }
        }

        if (TlelliHealth.flame <= 0)
        {
            TlelliHealth.SetHPDamage(1);
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
