using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class HeavyController : MonoBehaviour
{
    public float velRotacion = 20f;
    float BuscarRadio;
    public float jumpRadio;
    float radioGrande;
    public float radioDef = 10f;
    public float movSpeed;
    float movSpeedDef;
    float movSpeedMod = 20f;
    float movAcceMod = 5f;
    float movAcceDef;
    public float movAcce = 8;
    public bool isJumping =false;
    public Vector3 velocidad;

    Vector3 slamLand;
    Vector3 kbDirection;
    [HideInInspector] public Vector3 EnemySpawn;

    Transform target;
    NavMeshAgent navAgent;
    //TlelliFlameHealth flama;
    TleliHealth flama;
    HeavyAttack enemyStagger;
    HeavyAttack attack;
    public bool isAttacking;

    MusicaDinamica activa;
    bool knockback;
    float knockbackForce;

    bool jumping;
    bool atSpawn;

    public GameObject alertIcon;
    int alertActive;
    float reactionTime;

    HeavyBoiAnimationController heavyBoiAnimationController; //moe

    TleliDeath isTleliDead; //moe

    void Start()
    {
        heavyBoiAnimationController = GetComponentInChildren<HeavyBoiAnimationController>(); //moe
        attack = GetComponentInParent<HeavyAttack>();
        target = PlayerManager.instance.player.transform;
        BuscarRadio = radioDef;
        radioGrande = BuscarRadio * 1.5f;
        jumpRadio = radioGrande - 5f;

        EnemySpawn = this.transform.position;
        //flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        flama = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyStagger = GetComponent<HeavyAttack>();
        navAgent.speed = movSpeed;
        navAgent.acceleration = movAcce;
        movSpeedDef = movSpeed;
        movAcceDef = movAcce;
        atSpawn = true;
        isTleliDead = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliDeath>(); //moe
    }

    private void FixedUpdate()
    {
        if (knockback)
        {
            navAgent.velocity = kbDirection * 10f;
        }

        if (jumping)
        {
            navAgent.velocity = kbDirection * -2f;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= BuscarRadio && isJumping ==false && isTleliDead.isDead == false)
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
                //Debug.Log("is walking");
                heavyBoiAnimationController.IsWalkingBoolParameter(true); //moe
                BuscarRadio = radioGrande;
                isAttacking = true;
                atSpawn = false;



                if (distance >= jumpRadio && distance <= radioGrande && isJumping == false)
                {

                    int randomNum = Random.Range(1, 100);
                    if(randomNum >= 99)
                    {
                        isJumping = true;
                       
                    }
                }

                if(isJumping == true)
                {
                    
                    //Debug.Log("Funcionaelsaltin");
                    
                    
                    heavyBoiAnimationController.JumpTrigger(); //moe 
                    stopMov(5f);
                    navAgent.speed= movSpeedMod;
                    navAgent.acceleration = movAcceMod;
                    navAgent.SetDestination(target.position);
                    
                   // slamLand = new Vector3(target.position.x, target.position.y, target.position.z);
                   // transform.position = Vector3.MoveTowards(transform.position, slamLand, 100 * Time.deltaTime);


                }

                if (isTleliDead.isDead == true)
                {
                    navAgent.SetDestination(EnemySpawn);
                    isAttacking = false;
                }

                if (distance <= navAgent.stoppingDistance)
                {
                   
                    FaceTarget();
                    navAgent.SetDestination(target.position);
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

        if (navAgent.remainingDistance < 2.3 && !isAttacking && !atSpawn) //Draaek. This code ensures enemy returns to idle on spawn point
        {
            heavyBoiAnimationController.IsWalkingBoolParameter(false);
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
        heavyBoiAnimationController.IsWalkingBoolParameter(false); //Draaek
        yield return new WaitForSeconds(time);
        navAgent.speed = movSpeed;
        heavyBoiAnimationController.IsWalkingBoolParameter(true); //Draaek

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

    public void JumpAnimOff()
    {
        isJumping = false;
        navAgent.speed = movSpeedDef;
        navAgent.acceleration= movAcceDef;
    }

    IEnumerator slowMovCoroutine(float time)
    {
        navAgent.speed = movSpeed* 0.75f;
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

        yield return new WaitForSeconds(.2f);

        knockback = false;
        enemyStagger.isDisplaced = false;
        navAgent.speed = movSpeed;
        navAgent.angularSpeed = 120f;
        navAgent.acceleration = 8f;
        velRotacion = 20f;
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
