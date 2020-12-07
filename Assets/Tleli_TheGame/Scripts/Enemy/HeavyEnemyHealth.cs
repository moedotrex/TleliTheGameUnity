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
    
    public GameEvent eventLlave;

    //public Color ogColor;
    ParticleSystem particles;

    HeavyBoiAnimationController heavyBoiController; //Draaek

    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();
        enemyMov = GetComponent<HeavyController>();
        heavyBoiController = GetComponentInChildren<HeavyBoiAnimationController>(); //Draaek
    }


    void Update()
    {
        //MUERTE
        if (currentHealth <= 0 && imDead == false)
        {
            imDead = true;
            heavyBoiController.IsDeadTrigger();
            enemyMov.enabled = false;
            //OBTENER LLAVE
            eventLlave.LlaveHeavyBoi();
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
    }

    IEnumerator imAtuallyDying()
    {
        yield return new WaitForSeconds(TimeofDeath);

        Instantiate(flameSpawner, transform.position, Quaternion.identity);
        
        flama.BattleMode(false);
        Destroy(gameObject);
    }
}
