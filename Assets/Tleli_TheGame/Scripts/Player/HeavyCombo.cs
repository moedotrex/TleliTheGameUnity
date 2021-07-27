using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HeavyCombo : MonoBehaviour
{
    /// Adrián y Ciel

    List<string> animList = new List<string>(new string[] { "HAttack_1", "HAttack_2" }); 
    Animator animator;
    TleliDeath tleliDeath; //Stop actions when Tleli is Dead. By Emil.
    TlelliSonido SendSound; //ADRIAN

    int combonum;
    float reset;
    public float attackRate;
    private float nextAttackTime;
    float intResetTime;
    public float resetTime;

    public PlayerController Tleli;

    Ray shootRay;
    RaycastHit hit;
    public float range;
    public LayerMask mask;

    public float Damage;

    public GameObject macahuitlAnim;

    ///
    public float HAttackTime = 2;
    private float HAttackTimer = 0;
    ///

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        tleliDeath = GetComponent<TleliDeath>();
        SendSound = GetComponent<TlelliSonido>(); //ADRIAN
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && combonum < 2 && !tleliDeath.isDead && Tleli.isGrounded)
        {
            if (Time.time >= nextAttackTime)
            {
                //macahuitlAnim.SetActive(true);
                nextAttackTime = Time.time + attackRate;
                animator.SetTrigger(animList[combonum]);
                combonum++;
                reset = 0f;

                Debug.DrawRay(transform.position, transform.forward, Color.red);

            }
        }

        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > intResetTime)
            {
                animator.SetTrigger("Reset_HeavyCombo");
                combonum = 0;
                macahuitlAnim.SetActive(false);

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

    public void Attack()
    {
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out hit, range, mask))
        {

            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            EnemyController mov = hit.transform.GetComponent<EnemyController>();
            HeavyEnemyHealth hEnemy = hit.transform.GetComponent<HeavyEnemyHealth>();
            HeavyController hEnemyMov = hit.transform.GetComponent<HeavyController>();

            if (enemy != null)
            {
                enemy.HurtEnemy(Damage);
                mov.StartKnockBack();
                SendSound.CallThud();
            }
            else if (hEnemy != null)
            {
                hEnemy.HurtEnemy(Damage);
                SendSound.CallThud();
                // hEnemyMov.StartKnockBack();
            }
        }
    }

    public void turnOnHeavy()
    {
        macahuitlAnim.SetActive(true);
    }

    public void turnOffHeavy()
    {
        macahuitlAnim.SetActive(false);
    }
}
