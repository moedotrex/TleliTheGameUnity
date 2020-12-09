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

    public int hitCounter;

    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();
        enemyMov = GetComponent<HeavyController>();
        heavyBoiController = GetComponentInChildren<HeavyBoiAnimationController>(); //Draaek
        SendSound = GetComponent<EnemySounds>();
        hitCounter = 0;
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
            //heavyBoiController.IsHitTrigger();

            if (hitCounter < 1)
            {

                heavyBoiController.IsHitTrigger();
                hitCounter++;

            }
        }
    }

    public void actuallyDie()
    {
        StartCoroutine(imAtuallyDying());
    }

    IEnumerator imAtuallyDying()
    {
        yield return new WaitForSeconds(TimeofDeath);

        Instantiate(flameSpawner, transform.position, Quaternion.identity);
        
        flama.BattleMode(false);
        Destroy(gameObject);

        //OBTENER LLAVE
        GameEvent.gotLlave = true;
    }
}
