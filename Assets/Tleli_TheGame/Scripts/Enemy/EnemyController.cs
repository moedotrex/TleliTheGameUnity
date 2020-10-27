using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public bool touching;

    public float velRotacion = 20f;
    public float BuscarRadio;
    public float radioGrande;
    public float radioDef = 10f;
    public float movSpeed;

    public Vector3 EnemySpawn;

    Transform target;
    NavMeshAgent agente;
    NavMeshAgent nav;
    TlelliFlameHealth flama;

   public bool isAttacking;

    MusicaDinamica activa;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agente = GetComponent<NavMeshAgent>();
        BuscarRadio = radioDef;
        radioGrande = BuscarRadio * 1.5f;
        EnemySpawn = this.transform.position;
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = movSpeed;

    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= BuscarRadio)
            {
                agente.SetDestination(target.position);
                BuscarRadio = radioGrande;
                isAttacking = true;

                if (distance <= agente.stoppingDistance)
                {
                    FaceTarget();
                }

                flama.EnemyDistance(distance);
            }

            if (distance >= BuscarRadio && isAttacking == true)
            {
            BuscarRadio = radioDef;
            stopMov(5f);
            agente.SetDestination(EnemySpawn);
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
            nav.speed = 0f;
            yield return new WaitForSeconds(time);
            nav.speed = movSpeed;

        }

    public void slowMov(float time)
    {
        StartCoroutine(slowMovCoroutine(time));
    }

    IEnumerator slowMovCoroutine(float time)
    {
        nav.speed = movSpeed * 0.75f;
        yield return new WaitForSeconds(time);
        nav.speed = movSpeed;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AOE_Slow"))
        {
            slowMov(5f);
        }
    }
}
