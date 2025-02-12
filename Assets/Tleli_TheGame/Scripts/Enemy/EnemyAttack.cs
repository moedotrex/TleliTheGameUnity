﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;
    public float HeavyAttackDamage = 1;


    GameObject player;
    // TlelliFlameHealth TlelliHealth;
    TleliHealth TlelliHealth;
    tleliKnockBack playerKnockback;
    EnemyController enemyController;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    //[HideInInspector] public bool isDisplaced;
     public bool isDisplaced;
    //[HideInInspector] public bool isAnimating;
     public bool isAnimating;
    //crear evento para detectar tiempo de anim gethit y death

    ChomperAnimationController chomperController; //Draaek

    ParticleSystem trails; // Jules
    ParticleSystem trailsL; // Jules
    ChomperSounds SendSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        chomperController = GetComponentInChildren<ChomperAnimationController>(); //Draaek
        enemyController = GetComponent<EnemyController>();

        // TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        TlelliHealth = player.GetComponent<TleliHealth>();

        playerKnockback = player.GetComponent<tleliKnockBack>();
        enemyHealth = GetComponent<EnemyHealth>();

        SendSound = GetComponent<ChomperSounds>();

        trails = GameObject.Find("SpikeTrailsR").GetComponent<ParticleSystem>(); // Jules
        trailsL = GameObject.Find("SpikeTrailsL").GetComponent<ParticleSystem>();
    }


    void Update()
    {
        if (isAnimating == false)
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeBetweenAttacks && playerInRange && !isDisplaced && !enemyHealth.imDead)
        {

            int randomNum = Random.Range(0, 100);
            
            if (randomNum <= 50)
            {

                //  Attack();
                timer = 0f;
                isAnimating = true;
                enemyController.stopMov(2.14f);
                SendSound.Chompligth();
                chomperController.LightAtkTrigger(); //Draaek
            }

            if (randomNum > 51)
            {

                //  Attack();
                timer = 0f;
                isAnimating = true;
                enemyController.stopMov(1.17f);
                SendSound.ChompCharged();
                chomperController.HeavyAtkTrigger(); //Draaek
            }


        }
    }


    public void Attack()
    {
        timer = 0f;

        if (TlelliHealth.flame > 0 && playerInRange)
        {
            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack(7f);
        }

        if (TlelliHealth.flame <= 0 && playerInRange)
        {
            TlelliHealth.SetHPDamage(1);
        }
    }

    public void HeavyAttack()
    {
        timer = 0f;

        if (TlelliHealth.flame > 0 && playerInRange)
        {
            StartCoroutine(Trails()); //Jules
            StartCoroutine(TrailsL()); //Jules
            TlelliHealth.HurtFlame(attackDamage);
            playerKnockback.startKnockBack(10f);
            
        }

        if (TlelliHealth.flame <= 0 && playerInRange)
        {
            StartCoroutine(Trails()); //Jules
            StartCoroutine(TrailsL()); //Jules
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

    void AnimationOff()
    {
        isAnimating = false;
    }

    IEnumerator Trails()
    {
        yield return new WaitForSeconds(0.1f);
        trails.Emit(5);
    }

    IEnumerator TrailsL()
    {
        yield return new WaitForSeconds(0.1f);
        trailsL.Emit(5);
    }
}
