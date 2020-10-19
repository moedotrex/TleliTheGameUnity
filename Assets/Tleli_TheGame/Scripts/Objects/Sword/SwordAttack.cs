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
    }

    IEnumerator AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isAttacking", false);

    }
}
