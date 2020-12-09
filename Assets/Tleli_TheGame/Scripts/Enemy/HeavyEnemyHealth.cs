using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyHealth : MonoBehaviour
{

    public float health;
    float currentHealth;
    TlelliFlameHealth flama; //Added by Emil. Necessary for changing camera into Battle Mode.
    HeavyController enemyMov;
    SpawnBrokenWall door;

    public bool imDead;
    public float TimeofDeath;

    public bool imDestroyer;

    //public GameObject cloudSpawner;
    public GameObject flameSpawner;

    //public Color ogColor;
    ParticleSystem particles;
    public Material mat;
    float dLevel;

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
        dLevel = -1.5f;
        mat.SetFloat("_desintegrate", dLevel);
        hitCounter = 0;
        door = GameObject.FindGameObjectWithTag("SpecialDoor").GetComponent<SpawnBrokenWall>();
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

            if(imDestroyer)
            {
                door.SpawnObject();
            }

        }
        if (imDead == true)
        {
            dLevel = Mathf.Lerp(dLevel, 0.69f, Time.deltaTime/1.69f);
            mat.SetFloat("_desintegrate", dLevel);
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
        StartCoroutine(dissolve());
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

    IEnumerator dissolve()
    {
        dLevel = 1.5f;
        yield return new WaitForSeconds(1f);
    }
}
