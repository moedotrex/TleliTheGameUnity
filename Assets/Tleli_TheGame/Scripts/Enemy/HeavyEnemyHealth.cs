using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyHealth : MonoBehaviour
{

    public float health;
    float currentHealth;
    TlelliFlameHealth flama; //Added by Emil. Necessary for changing camera into Battle Mode.
    HeavyController enemyMov;

    public bool imDead;
    public float TimeofDeath;

    //public GameObject cloudSpawner;
    public GameObject flameSpawner;

    //public Color ogColor;
    ParticleSystem particles;

    HeavyBoiAnimationController heavyBoiController; //Draaek
    EnemySounds SendSound;



    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();
        enemyMov = GetComponent<HeavyController>();
        heavyBoiController = GetComponentInChildren<HeavyBoiAnimationController>(); //Draaek
        SendSound = GetComponent<EnemySounds>();
    }


    void Update()
    {
        //MUERTE
        if (currentHealth <= 0 && imDead == false)
        {
            imDead = true;
            SendSound.heavyDead();
            heavyBoiController.IsDeadTrigger();
            enemyMov.enabled = false;
        }
    }

    public void HurtEnemy(float damage)
    {

        if (imDead == false)
        {
            //  GameObject.Instantiate(blood, transform.position, Quaternion.identity);
            currentHealth -= damage;
            particles.Emit((int)currentHealth);
            heavyBoiController.IsHitTrigger();
        }
    }

    public void actuallyDie()
    {
        StartCoroutine(imAtuallyDying());
        //OBTENER LLAVE
        GameEvent.gotLlave = true;
    }

    IEnumerator imAtuallyDying()
    {
        yield return new WaitForSeconds(TimeofDeath);

        Instantiate(flameSpawner, transform.position, Quaternion.identity);
        
        flama.BattleMode(false);
        Destroy(gameObject);
    }
}
