using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LightCombo : MonoBehaviour
{
    List<string> animList = new List<string>(new string[] { "LAttack_1", "LAttack_2", "LAttack_3", "LAttack_4" });
    Animator animator;
    int combonum;
    float reset;
    float intResetTime;
    public float resetTime;

    public float attackRate;
    float nextAttackTime;

    public float LAttackTime = 2;
    private float LAttackTimer = 0;

    Ray shootRay;
    RaycastHit hit;
    public float range;
    public LayerMask mask;

    public float Damage;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0) && combonum < 3)
        {
            if (Time.time >= nextAttackTime)
            {
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                nextAttackTime = Time.time + attackRate;
                animator.SetTrigger(animList[combonum]);
                combonum++;
                reset = 0f;
                //Debug.Log(combonum);
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
                animator.SetTrigger("Reset_LightCombo");
                combonum = 0;
                Debug.Log("combo reset");
            }
        }
        if (combonum == 4)
        {
            intResetTime = 4f;
            combonum = 0;
        }
        else
        {
            intResetTime = resetTime;
        }


        if (Input.GetMouseButton(0))
        {
            LAttackTimer += Time.deltaTime;
        }


        if (LAttackTimer >= LAttackTime && !Input.GetMouseButton(1))
        {
            UnityEngine.Debug.Log("CHARGING ... LIGHT!");
            animator.SetBool("Lcharge", true);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1))
        {
            UnityEngine.Debug.Log("RELEASE!   LIGHT ");
            animator.SetBool("Lcharge", false);
            LAttackTimer = 0;
        }
    }
}