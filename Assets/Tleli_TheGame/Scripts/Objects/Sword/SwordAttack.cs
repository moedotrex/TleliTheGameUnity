using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        animator = GetComponent<Animator>();
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
                

                if (Physics.Raycast(shootRay, out hit, range, mask))
                {

                    EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                    EnemyController mov = hit.transform.GetComponent<EnemyController>();
                    TransporterStaticEnemy TP = hit.transform.GetComponent<TransporterStaticEnemy>();

                    Debug.DrawLine(transform.position, hit.point, Color.red);

                    if (enemy != null)
                    {
                        enemy.HurtEnemy(Damage);
                        
                        
                    }
                    if (mov != null)
                    {
                        
                        mov.stopMov(5f);

                    }
                    if (TP != null)
                    {
                        TP.transport();
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
