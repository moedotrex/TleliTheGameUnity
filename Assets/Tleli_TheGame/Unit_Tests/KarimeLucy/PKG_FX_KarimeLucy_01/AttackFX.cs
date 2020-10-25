using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Karime y Lucy. Controla emisión de partículas desde el ParticleSystem de un enemigo y de los efectos del arma.

public class AttackFX: MonoBehaviour
{
    //Partículas del enemigo
    ParticleSystem particles;      
    EnemyHealth enemyHealth;

    //Efectos del arma
    VisualEffect sparks;           
    ParticleSystem trails;
    ParticleSystem weaponParticles;
    ParticleSystem slash;

    public bool useSparks;
    public bool useTrails;
    public bool useParticles;
    public bool useSlash;
    public bool useMovingTrails;


    void Start()
    {
        if (useSparks || useMovingTrails)
        {
           sparks = GameObject.Find("SwordSparks").GetComponent<VisualEffect>();
            if (useMovingTrails)
            {
                sparks.SendEvent("StartMovingTrails");
            }
            else
            {
                sparks.SendEvent("StopMovingTrails");
            }
        }
        if (useTrails)
        {
            trails = GameObject.Find("WeaponTrails").GetComponent<ParticleSystem>();
        }
        if (useParticles)
        {
            weaponParticles = GameObject.Find("WeaponParticles").GetComponent<ParticleSystem>();
        }
        if (useSlash)
        {
            slash = GameObject.Find("WeaponSlash").GetComponent<ParticleSystem>();
        } 
    }


    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            if (useTrails)
            {
                trails.Emit(30);
            }
            if (useParticles)
            {
                weaponParticles.Emit(20);
            }
            if (useSlash)
            {
                slash.Emit(1);
            }   
        }
    }

    public void PlayFX(EnemyHealth enem)
    {
        if (useSparks)
        {
            StartCoroutine(WeaponFX());
        }
        GameObject enemy = enem.gameObject;
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        particles = enemy.GetComponentInChildren<ParticleSystem>();
        float enemHP = enemyHealth.currentHealth;
        particles.Emit((int)(enemHP * (enemHP / 5)));
    }
    

    IEnumerator WeaponFX()
    {
        sparks.SendEvent("Hit");
        yield return new WaitForSeconds(0.3f);
        sparks.SendEvent("SparksStop");
    }

  
}
