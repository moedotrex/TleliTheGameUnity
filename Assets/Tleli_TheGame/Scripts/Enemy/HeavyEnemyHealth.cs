using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyHealth : MonoBehaviour
{

    public float health;
    float currentHealth;
    TlelliFlameHealth flama; //Added by Emil. Necessary for changing camera into Battle Mode.

    public bool imPoisonous;
    public GameObject cloudSpawner;
    public GameObject flameSpawner;

    public GameEvent eventLlave;

    //public Color ogColor;
    ParticleSystem particles;

    ChomperAnimationController chomperController; //Draaek

    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();

        //chomperController = GetComponentInChildren<ChomperAnimationController>(); //Draaek
    }


    void Update()
    {
        //MUERTE
        if (currentHealth <= 0)
        {
            //chomperController.IsDeadBoolParameter(true);
            Instantiate(flameSpawner, transform.position, Quaternion.identity);

            if (imPoisonous)
            {
                Instantiate(cloudSpawner, transform.position, Quaternion.identity);
            }

            flama.BattleMode(false);
            Destroy(gameObject);

            //OBTENER LLAVE
            eventLlave.LlaveHeavyBoi();
        }
    }

    public void HurtEnemy(float damage)
    {
        //  GameObject.Instantiate(blood, transform.position, Quaternion.identity);
        currentHealth -= damage;
        particles.Emit((int)currentHealth);

       /* if (Random.Range(0, 10) > 5)
        {
            chomperController.IsHitTrigger();
        }

        else
        {
            chomperController.IsHitAltTrigger();
        }*/
        // Debug.Log(transform.name + "takes" + damage + "damage.");
        //StartCoroutine(HurtEnemyCoroutine());
    }
}
