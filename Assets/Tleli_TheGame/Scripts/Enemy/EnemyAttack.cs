using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;

    GameObject player;
    TlelliFlameHealth TlelliHealth;
    bool playerInRange;
    float timer;
    //crear evento para detectar tiempo de anim gethit y death

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TlelliHealth = player.GetComponent<TlelliFlameHealth>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }


    void Attack()
    {
        timer = 0f;

        if (TlelliHealth.HP > 0)
        {
            TlelliHealth.SetHPDamage(attackDamage);
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
