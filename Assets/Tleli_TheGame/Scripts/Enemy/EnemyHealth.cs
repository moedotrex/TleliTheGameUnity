using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float currentHealth;
    TlelliFlameHealth flama; //Added by Emil. Necessary for changing camera into Battle Mode.
    EnemyController enemyMov;

    public bool imPoisonous;
    public GameObject cloudSpawner;
    public GameObject flameSpawner;
    public bool imDead;

    public float TimeofDeath;

    public bool isIkniEvent = false;
    public GameEvent ikniEvent;

    //public Color ogColor;
    ParticleSystem particles;

    ChomperAnimationController chomperController; //Draaek

    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();
        enemyMov = GetComponent<EnemyController>();

        chomperController = GetComponentInChildren<ChomperAnimationController>(); //Draaek
    }


    void Update()
    {
        if (currentHealth <= 0 && imDead == false)
        {
            imDead = true;
           // chomperController.IsDeadBoolParameter(true);
            chomperController.IsDeadTrigger();
            enemyMov.enabled = false;

        }

   
    }

    public void HurtEnemy(float damage)
    {
        //  GameObject.Instantiate(blood, transform.position, Quaternion.identity);
        currentHealth -= damage;
        particles.Emit((int)currentHealth);

        if (Random.Range(0, 10) > 5)
        {
            chomperController.IsHitTrigger();
        }

        else
        {
            chomperController.IsHitAltTrigger();
        }
        // Debug.Log(transform.name + "takes" + damage + "damage.");
        //StartCoroutine(HurtEnemyCoroutine());
    }

    /* IEnumerator HurtEnemyCoroutine()
     {
         Component[] rend = gameObject.GetComponentsInChildren<Renderer>();

         foreach (Component layer in rend)
         {
             Renderer r = layer.GetComponent<Renderer>();
             r.material.color = Color.red;
         }

         yield return new WaitForSeconds(0.1f);

         foreach (Component layer in rend)
         {
             Renderer r = layer.GetComponent<Renderer>();
             r.material.color = ogColor;

         }
     }*/

    public void actuallyDie()
    {
        Debug.Log("ided");
        StartCoroutine(imAtuallyDying());

        //CHEQUEO SI SON ENEMIGOS DE EVENTO IKNI
        if (isIkniEvent)
        {
            ikniEvent.TutorialSaveIkni();
        }
    }

    IEnumerator imAtuallyDying()
    {
        yield return new WaitForSeconds(TimeofDeath);

        Instantiate(flameSpawner, transform.position, Quaternion.identity);

        if (imPoisonous)
        {
            Instantiate(cloudSpawner, transform.position, Quaternion.identity);
        }

        flama.BattleMode(false);
        Destroy(gameObject);
    }
}
