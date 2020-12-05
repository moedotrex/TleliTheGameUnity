using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float currentHealth;
    TlelliFlameHealth flama; //Added by Emil. Necessary for changing camera into Battle Mode.

    public bool imPoisonous;
    public GameObject cloudSpawner;
    public GameObject flameSpawner;

    public Color ogColor;
    ParticleSystem particles;

    ChomperAnimationController chomperController; //Draaek

    void Start()
    {
        currentHealth = health;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        particles = GetComponentInChildren<ParticleSystem>();

        chomperController = GetComponentInChildren<ChomperAnimationController>(); //Draaek
    }


    void Update()
    {
        if (currentHealth <= 0)
        {
            chomperController.IsDeadBoolParameter(true);
            Instantiate(flameSpawner, transform.position, Quaternion.identity);

            if (imPoisonous)
            {
                Instantiate(cloudSpawner, transform.position, Quaternion.identity);
            }

            flama.BattleMode(false);
            Destroy(gameObject);
        }
    }

    public void HurtEnemy(float damage)
    {
        //  GameObject.Instantiate(blood, transform.position, Quaternion.identity);

        if (Random.Range(0, 10) > 5)
        {
            chomperController.IsHitTrigger();
        }

        else
        {
            chomperController.IsHitAltTrigger();
        }
        currentHealth -= damage;
        particles.Emit((int)currentHealth);
        // Debug.Log(transform.name + "takes" + damage + "damage.");
        StartCoroutine(HurtEnemyCoroutine());
    }

    IEnumerator HurtEnemyCoroutine()
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
    }
}
