using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muleKick : MonoBehaviour
{
    public float timeBetweenAttacks;
    public float attackDamage = 1;
    public float attackDamageSlam = 1;
    public float KBforce;
    public float beforeKickTime;

    GameObject player;
    GameObject enemy;
    //TlelliFlameHealth TlelliHealth;
    TleliHealth TlelliHealth;

    tleliKnockBack playerKnockback;

    bool playerInRange;
    float timer;
    public bool isDisplaced;
    //crear evento para detectar tiempo de anim gethit y death

    HeavyBoiAnimationController heavyBoiAnimationController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("HeavyEnemy");
        //TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        TlelliHealth = player.GetComponent<TleliHealth>();

        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //slamLand = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);

        playerKnockback = player.GetComponent<tleliKnockBack>();

        heavyBoiAnimationController = enemy.GetComponentInChildren<HeavyBoiAnimationController>();

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
        if (TlelliHealth.flame > 0 && playerInRange)
        {
            heavyBoiAnimationController.KickTrigger();
            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack(KBforce);
        }

        if (TlelliHealth.flame <= 0)
        {
            heavyBoiAnimationController.KickTrigger();
            TlelliHealth.SetHPDamage(1);
            playerKnockback.startKnockBack(KBforce);
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

    IEnumerator waitKick()
    {
        yield return new WaitForSeconds(beforeKickTime);
    }
}
