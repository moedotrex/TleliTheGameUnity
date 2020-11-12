using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Karime y Lucy  11/oct: Se agregó referencia a las partículas del enemigo (Todo lo que tiene //------ al final, corresponde a esta funcionalidad)

public class SwordAttack : MonoBehaviour
{

    Animator animator;

    public float MArate = 0f;
    private float ArateTimeStamp = 0f;

    [FMODUnity.EventRef]
    public string inputattacksound;

    Ray shootRay;
    RaycastHit hit;
    public float range;
    public LayerMask mask;
    public float kBack = 200f;

    public float Damage;

    AttackFX enemyParticles;  //---------
  
    /// AdrianCiel
    ///
    public float LAttackTime = 2;
    public float HAttackTime = 2;
    private float LAttackTimer = 0;
    private float HAttackTimer = 0;
    ///

    void Start()
    {
        animator = GetComponent<Animator>();
        //enemyParticles = GameObject.FindGameObjectWithTag("Sword").GetComponent<AttackFX>();   //------------
        enemyParticles = GameObject.Find("Player_Weapon").GetComponent<AttackFX>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > ArateTimeStamp)
            {

               StartCoroutine(AttackAnimation());
                FMODUnity.RuntimeManager.PlayOneShot(inputattacksound);
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;
                ArateTimeStamp = Time.time + MArate;

                Debug.DrawRay(transform.position, transform.forward, Color.red);


                if (Physics.Raycast(shootRay, out hit, range, mask))
                {

                    EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                    EnemyController mov = hit.transform.GetComponent<EnemyController>();

                   

                    if (enemy != null)
                    {
                        enemy.HurtEnemy(Damage);
                        enemyParticles.PlayFX(enemy);   //--------------
                        mov.stopMov(0.2f);
                    }
                    if (hit.rigidbody != null)
                    {

                        hit.rigidbody.AddForce(-hit.normal * kBack);
                    }

                }
            }
        }
        ///----------------------------------------------------------------

        if (Input.GetMouseButtonDown(1) /*&& !Input.GetMouseButton(0)*/)
        {
            if (Time.time > ArateTimeStamp)
            {

                StartCoroutine(HAttackAnimation());
                //FMODUnity.RuntimeManager.PlayOneShot(inputattacksound);
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;
                ArateTimeStamp = Time.time + MArate;

                Debug.DrawRay(transform.position, transform.forward, Color.red);


                if (Physics.Raycast(shootRay, out hit, range, mask))
                {

                    EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                    EnemyController mov = hit.transform.GetComponent<EnemyController>();



                    if (enemy != null)
                    {
                        enemy.HurtEnemy(Damage);
                        mov.stopMov(0.2f);
                    }
                    if (hit.rigidbody != null)
                    {

                        hit.rigidbody.AddForce(-hit.normal * kBack);
                    }

                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            LAttackTimer += Time.deltaTime;
        }

        /*if (Input.GetMouseButton(1))
        {
            HAttackTimer += Time.deltaTime;
        }*/

        if (LAttackTimer >= LAttackTime && !Input.GetMouseButton(1))
        {
            //UnityEngine.Debug.Log("LIGHT!");
            animator.SetBool("Lcharge", true);
        }

        if (HAttackTimer >= HAttackTime)
        {
            //UnityEngine.Debug.Log("HEAVY!");
            animator.SetBool("HCharge", true);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1))
        {
            //UnityEngine.Debug.Log("RELEASE!L");
            animator.SetBool("Lcharge", false);
            LAttackTimer = 0;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //UnityEngine.Debug.Log("RELEASE H!");
            animator.SetBool("HCharge", false);
            HAttackTimer = 0;
        }
        ///----------------------------------------------------------------
    }

    IEnumerator AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isAttacking", false);

    }
    IEnumerator HAttackAnimation()
    {
        animator.SetBool("HAttack", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("HAttack", false);

    }
}
