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

    void Start()
    {
        sparks = GetComponentInChildren<VisualEffect>();       
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
        sparks.SendEvent("Hit");
        yield return new WaitForSeconds(0.3f);
        sparks.Stop();
    }

}
