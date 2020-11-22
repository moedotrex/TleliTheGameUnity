using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;

    GameObject player;
    // TlelliFlameHealth TlelliHealth;
    TleliHealth TlelliHealth;
    tleliKnockBack playerKnockback;
    bool playerInRange;
    float timer;
    public bool isDisplaced;
    //crear evento para detectar tiempo de anim gethit y death

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        TlelliHealth = player.GetComponent<TleliHealth>();

        playerKnockback = player.GetComponent<tleliKnockBack>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && isDisplaced == false)
        {
            Attack();
        }
    }


    void Attack()
    {
        timer = 0f;

        if (TlelliHealth.flame > 0)
        {
            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack();
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
