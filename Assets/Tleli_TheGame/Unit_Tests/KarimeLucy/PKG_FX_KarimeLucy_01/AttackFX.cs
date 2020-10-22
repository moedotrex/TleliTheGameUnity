using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Karime y Lucy 11 oct. Controla emisión de partículas desde el ParticleSystem de un enemigo y del VisualEffect del arma.

public class AttackFX: MonoBehaviour
{

    VisualEffect sparks;           //Efecto del arma 
    ParticleSystem particles;      //ParticleSystem del enemigo
    EnemyHealth enemyHealth;
    ParticleSystem trails;
    ParticleSystem weaponParticles;

    void Start()
    {
        sparks = GameObject.Find("SwordSparks").GetComponent<VisualEffect>();
        trails = GameObject.Find("WeaponTrails").GetComponent<ParticleSystem>();
        weaponParticles = GameObject.Find("WeaponParticles").GetComponent<ParticleSystem>();
    }


    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            trails.Emit(30);
            weaponParticles.Emit(20);
        }
    }

    public void PlayFX(EnemyHealth enem)
    {
        StartCoroutine(WeaponFX());
        GameObject enemy = enem.gameObject;
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        particles = enemy.GetComponentInChildren<ParticleSystem>();
        float enemHP = enemyHealth.currentHealth;
        particles.Emit((int)(enemHP * (enemHP / 5)));
    }
    

    IEnumerator WeaponFX()
    {
        //yield return new WaitForSeconds(0.3f);
        sparks.SendEvent("Hit");
        yield return new WaitForSeconds(0.3f);
        //sparks.Stop();
        sparks.SendEvent("SparksStop");
    }

  
}
