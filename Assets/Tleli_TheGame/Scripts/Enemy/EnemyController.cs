using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float velRotacion = 20f;
    float BuscarRadio;
    float radioGrande;
    public float radioDef = 10f;
    public float movSpeed;

    Vector3 kbDirection;
    public Vector3 EnemySpawn;
    bool atSpawn;

    Transform target;
    NavMeshAgent navAgent;
    //TlelliFlameHealth flama;
    TleliHealth flama;
    EnemyAttack enemyStagger;

    public bool isAttacking;

    MusicaDinamica activa;
    bool knockback;
    float knockbackForce;

    public GameObject alertIcon;
    int alertActive;
    float reactionTime;

    float currentX;
    float currentY;

    ChomperAnimationController chomperController; //Draaek

    void Start()
    {
        chomperController = GetComponentInChildren<ChomperAnimationController>(); //Draaek

        target = PlayerManager.instance.player.transform;
        BuscarRadio = radioDef;
        radioGrande = BuscarRadio * 1.5f;
        EnemySpawn = this.transform.position;
        //flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyStagger = GetComponent<EnemyAttack>();
        navAgent.speed = movSpeed;
        atSpawn = true;
        currentX = this.transform.position.x;
        currentY = this.transform.position.y;
    }

    private void FixedUpdate()
    {
        if (knockback)
        {
            navAgent.velocity = kbDirection * 3.5f;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= BuscarRadio)
        {

            reactionTime += Time.deltaTime;

            if (reactionTime >= 0.6f)
            {
                if (alertActive < 1)
                {
                    Instantiate(alertIcon, transform.position, Quaternion.identity);
                    alertActive++;
                }

                navAgent.SetDestination(target.position);
                atSpawn = false;

                chomperController.IsWalkingBoolParameter(true); //Draaek

                BuscarRadio = radioGrande;
                isAttacking = true;

                if (distance <= navAgent.stoppingDistance)
                {
                    FaceTarget();
                }

                flama.EnemyDistance(distance);
                flama.BattleMode(true); //Added by Emil. Necessary for changing camera into Battle Mode.
            }
        }

        if (distance >= BuscarRadio && isAttacking == true)
        {
            reactionTime = 0f;
            BuscarRadio = radioDef;
            stopMov(5f);
            navAgent.SetDestination(EnemySpawn);
            isAttacking = false;
            alertActive = 0;

            flama.BattleMode(false); //Added by Emil. Necessary for changing camera into Battle Mode.
        }

        if (navAgent.remainingDistance < 0.8 && !isAttacking && !atSpawn) //Draaek. This code ensures enemy returns to idle on spawn point
        {
            chomperController.IsWalkingBoolParameter(false);
            atSpawn = true;
        }
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
        navAgent.acceleration = 0;

        chomperController.IsWalkingBoolParameter(false); //Draaek
        yield return new WaitForSeconds(time);
        navAgent.speed = movSpeed;
        navAgent.acceleration = 8f;

        chomperController.IsWalkingBoolParameter(true); //Draaek
    }

    public void slowMov(float time)
    {
        StartCoroutine(slowMovCoroutine(time));
    }

    public void StartKnockBack()
    {
        kbDirection = transform.forward * -1;
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
        knockback = true;
        enemyStagger.isDisplaced = true;
        navAgent.speed = 10;
        navAgent.angularSpeed = 0;
        navAgent.acceleration = 0;
        velRotacion = 0f;

        yield return new WaitForSeconds(0.2f);

        knockback = false;
        enemyStagger.isDisplaced = false;
        navAgent.speed = movSpeed;
        navAgent.angularSpeed = 120f;
        navAgent.acceleration = 8f;
        velRotacion = 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AOE_Slow"))
        {
            slowMov(5f);
            Debug.Log("slowed");
        }
    }

}
