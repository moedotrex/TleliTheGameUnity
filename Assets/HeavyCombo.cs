using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCombo : MonoBehaviour
{
    /// Adrián y Ciel

    List<string> animList = new List<string>(new string[] { "HAttack_1", "HAttack_2" }); 
    Animator animator;
    int combonum;
    float reset;
    public float attackRate;
    private float nextAttackTime;
    float intResetTime;
    public float resetTime;

    Ray shootRay;
    RaycastHit hit;
    public float range;
    public LayerMask mask;

    public float Damage;

    ///
    public float HAttackTime = 2;
    private float HAttackTimer = 0;
    ///

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && combonum < 2)
        {
            if (Time.time >= nextAttackTime)
            {

                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                nextAttackTime = Time.time + attackRate;
                animator.SetTrigger(animList[combonum]);
                combonum++;
                reset = 0f;

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
                }
            }
        }

        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > intResetTime)
            {
                animator.SetTrigger("Reset_HeavyCombo");
                combonum = 0;
                Debug.Log("combo reset");
            }
        }
        if (combonum == 3)
        {
            intResetTime = 4f;
            combonum = 0;
        }
        else
        {
            intResetTime = resetTime;
        }

        if (Input.GetMouseButton(1))
        {
            HAttackTimer += Time.deltaTime;
        }

       /* if (HAttackTimer >= HAttackTime)
        {
            //UnityEngine.Debug.Log("HEAVY!");
            animator.SetBool("HCharge", true);

        }

        if (Input.GetMouseButtonUp(1))
        {
            //UnityEngine.Debug.Log("RELEASE H!");
            animator.SetBool("HCharge", false);
            HAttackTimer = 0;
        }*/
        ///----------------------------------------------------------------
    }
}
