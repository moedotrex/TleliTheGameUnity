using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public float velRotacion = 20f;
    float BuscarRadio;
    float radioGrande;
    public float radioDef = 10f;
    public float movSpeed;

    Vector3 direction;
    public float knockbackForce;

    Vector3 EnemySpawn;

    Transform target;
    NavMeshAgent navAgent;
    TlelliFlameHealth flama;

   public bool isAttacking;

    MusicaDinamica activa;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        BuscarRadio = radioDef;
        radioGrande = BuscarRadio * 1.5f;
        EnemySpawn = this.transform.position;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= BuscarRadio)
            {
            navAgent.SetDestination(target.position);
                BuscarRadio = radioGrande;
                isAttacking = true;

                if (distance <= navAgent.stoppingDistance)
                {
                    FaceTarget();
                }

                flama.EnemyDistance(distance);
            }

            if (distance >= BuscarRadio && isAttacking == true)
            {
            BuscarRadio = radioDef;
            stopMov(5f);
            navAgent.SetDestination(EnemySpawn);
            isAttacking = false;
            }

        /*if (isAttacking == true)
        {
            MusicaDinamica activa = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MusicaDinamica>();
            activa.tlelliEnCombate = true;
            Debug.Log("true");
        }
       if (isAttacking == false)
        {
            MusicaDinamica activa = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MusicaDinamica>();
            activa.tlelliEnCombate = false;
            Debug.Log("false");

        }*/
    }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velRotacion);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BuscarRadio);
        }

        public void stopMov(float time)
        {
            StartCoroutine(stopMovCoroutine(time));
        }

        IEnumerator stopMovCoroutine(float time)
        {
        navAgent.speed = 0f;
            yield return new WaitForSeconds(time);
        navAgent.speed = movSpeed;

        }

    public void slowMov(float time)
    {
        StartCoroutine(slowMovCoroutine(time));
    }

    public void StartKnockBack()
    {
            direction = transform.forward * -1;
            StartCoroutine(KnockBack());
    }

    IEnumerator slowMovCoroutine(float time)
    {
        navAgent.speed = movSpeed * 0.75f;
        yield return new WaitForSeconds(time);
        navAgent.speed = movSpeed;

    }

    IEnumerator KnockBack()
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.2f)
        {
            navAgent.velocity = direction * knockbackForce * Time.deltaTime;
            //navAgent.speed = 10;
            navAgent.angularSpeed = 0;
            navAgent.acceleration = 20;
            //velRotacion = 0f;

            yield return new WaitForSeconds(0.2f);

            //navAgent.speed = movSpeed;
            navAgent.angularSpeed = 120f;
            navAgent.acceleration = 8f;
            //velRotacion = 20f; */
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AOE_Slow"))
        {
            slowMov(5f);
            Debug.Log("slowed");
        }
    }
}
