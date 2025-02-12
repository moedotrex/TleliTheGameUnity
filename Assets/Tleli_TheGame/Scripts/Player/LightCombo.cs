﻿using System.Collections;
using System.Collections.Generic;
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
    float rayRange = 1f;
    public LayerMask mask;

    public float Damage;
    float currentDamage;

    Vector3 moveDir;
    PlayerController moveScript;

    public GameObject daggerAnim;
    public GameObject spearAnim;
    private Transform target;
    float attRange = 6f;
    float velRotacion = 200F;

    public bool lunging;
    public bool gotCharged;
    public bool imHolding;

    public PlayerController Tleli;

    ParticleSystem slash;   //------
    ParticleSystem trails;
    TlelliSonido SendLAttack; //ADRIAN
    TleliDeath tleliDeath; //Stop actions when Tleli is Dead. By Emil.

    public bool isAnimating = false;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        moveScript = GetComponent<PlayerController>();
        tleliDeath = GetComponent<TleliDeath>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        currentDamage = Damage;
        slash = GameObject.Find("WeaponSlash").GetComponent<ParticleSystem>();   //------
        trails = GameObject.Find("WeaponTrails").GetComponent<ParticleSystem>();
        SendLAttack = GetComponent<TlelliSonido>(); //ADRIAN

    }

    void Update()
    {
      if (!isAnimating)
        { 
        if (Input.GetMouseButtonDown(0) && combonum < 3 && !tleliDeath.isDead && Tleli.isGrounded)
        {
            if (Tleli.isGrounded == true)
            {
                Tleli.canMove = 0.5f;
            }

            if (Time.time >= nextAttackTime)
            {
                daggerAnim.SetActive(true);
                animator.SetBool("bool_ResetLightCombo", false);

                nextAttackTime = Time.time + attackRate;
                animator.SetTrigger(animList[combonum]);
                combonum++;
                //currentDamage += 2f;
                reset = 0f;


                if (combonum == 1 || combonum == 2)   //------ COMMENTED TO REDUCE ERRORS. DO NOT REMOVE.
                {
                    StartCoroutine(Slash());
                }
                trails.Emit(20);
            }

            if (target != null)
            {
                FaceTarget();
            }
        }
        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > intResetTime)
            {
                combonum = 0;
                moveScript.isAnimating = false;
                //animator.SetTrigger("Reset_LightCombo");
                animator.SetBool("bool_ResetLightCombo", true);


                //currentDamage = Damage;
                daggerAnim.SetActive(false);
                Debug.Log("combo reset");
            }
        }

        else
        {
            intResetTime = resetTime;
        }

            if (gotCharged && !tleliDeath.isDead && Tleli.isGrounded) // ya adquirio el poder? 
            {
                if (Input.GetMouseButton(0))
                {
                    LAttackTimer += Time.deltaTime;
                }

                if (LAttackTimer >= LAttackTime && !Input.GetMouseButton(1))
                {
                    moveScript.isAnimating = true;
                    moveScript.imHolding = true;
                    animator.SetBool("Lcharge", true);
                    spearAnim.SetActive(true);



                    //SendLAttack.LACharge = true;
                    //Debug.Log(animator.GetBool("Lcharge"));


                    if (target != null)
                    {
                        FaceTarget();
                    }
                }
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1))
                {
                    moveScript.isAnimating = false;
                    moveScript.imHolding = false;
                    animator.SetBool("Lcharge", false);
                    StartCoroutine(turnOffSpearCoroutine());
                    //Debug.Log(animator.GetBool("Lcharge"));
                    LAttackTimer = 0;
                    //endLAttack.LACharge = false;

                }
            }
        }

        if (lunging == true)
        {

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out hit, 2f, mask))
            {
                DestroyOnCollision wall = hit.transform.GetComponent<DestroyOnCollision>();
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                EnemyController mov = hit.transform.GetComponent<EnemyController>();
                HeavyController Hmov = hit.transform.GetComponent<HeavyController>();

                if (enemy != null)
                {
                    if (enemy.imDead == false)
                    {
                        Hmov.StartKnockBack();
                        enemy.HurtEnemy(currentDamage + 10f);
                        SendLAttack.CallThud();
                        //mov.StartKnockBack();
                    }
                }

                if (wall != null)
                {
                    Debug.Log("wall hit");
                    wall.DestroyYourself();
                }
            }
        }
    }


    public void Attack()
    {
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        SendLAttack.LAttack = true; //ADRIAN

        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(shootRay, out hit, rayRange, mask))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            EnemyController mov = hit.transform.GetComponent<EnemyController>();
            HeavyEnemyHealth hEnemy = hit.transform.GetComponent<HeavyEnemyHealth>();
            HeavyController hEnemyMov = hit.transform.GetComponent<HeavyController>();

            if (enemy != null)
            {
                if (enemy.imDead == false)
                {
                    enemy.HurtEnemy(currentDamage);
                    mov.StartKnockBack();
                    //SendLAttack.ThudOccurs=true;
                    SendLAttack.CallThud();
                }
            }
            else if (hEnemy != null)
            {
                hEnemy.HurtEnemy(currentDamage);
                SendLAttack.CallThud();
                // hEnemyMov.StartKnockBack();
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velRotacion);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    public void startAttForward(float animDuration)
    {
        StartCoroutine(AttForward(animDuration));
    }

    public void startChargedAttForward(float animDuration)
    {
        StartCoroutine(ChargedAttForward(animDuration));
    }

    IEnumerator AttForward(float animDuration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + animDuration)
        {
            moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward; //direccion tomada de player Y transform 
            moveScript.characterController.Move(moveDir * 5f * Time.deltaTime);

            // moveScript.isDisplaced = true;
            yield return null;
            //moveScript.isDisplaced = false;
        }
    }

    IEnumerator ChargedAttForward(float animDuration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + animDuration)
        {
            // moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
            moveDir = Camera.main.transform.forward; //direccion tomada de la camara
            moveScript.characterController.Move(moveDir * 15f * Time.deltaTime);
            moveScript.isDisplaced = true;
            yield return null;
            moveScript.isDisplaced = false;
        }
    }

    IEnumerator Slash()   //------
    {
        yield return new WaitForSeconds(0.15f);
        slash.Emit(1);
    }

    public void turnOffSpear()
    {
        spearAnim.SetActive(false);
    }

    IEnumerator turnOffSpearCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        spearAnim.SetActive(false);

    }
}