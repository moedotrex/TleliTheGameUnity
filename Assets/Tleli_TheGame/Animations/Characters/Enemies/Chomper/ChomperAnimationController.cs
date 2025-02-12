﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperAnimationController : MonoBehaviour
{
    Animator chomperAnimator;
    EnemyAttack enemyCombat;
    EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        chomperAnimator = this.gameObject.GetComponent<Animator>();
        enemyCombat = GetComponentInParent<EnemyAttack>();
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    public void IsHitTrigger()
    {
        chomperAnimator.SetTrigger("isHit");
    }

    public void IsHitAltTrigger()
    {
        chomperAnimator.SetTrigger("isHitAlt");
    }

    public void IsDeadTrigger()
    {
        chomperAnimator.SetTrigger("IsDeadT");
    }

    public void IsWalkingBoolParameter(bool isWalking)
    {
        chomperAnimator.SetBool("isWalking", isWalking);
    }

    public void LightAtkTrigger()
    {
        chomperAnimator.SetTrigger("LightAtkTrigger");
    }

    public void HeavyAtkTrigger()
    {
        chomperAnimator.SetTrigger("HeavyAtkTrigger");
    }

    public void IsDeadBoolParameter(bool isDead)
    {
        chomperAnimator.SetBool("isDead", isDead);
    }

    public void lightAnimationHit()
    {
        enemyCombat.Attack();
    }

    public void heavyAnimationHit()
    {
        enemyCombat.HeavyAttack();
    }

    public void turnOffAnim()
    {
        enemyCombat.isAnimating = false;
    }

    public void Die()
    {
        enemyHealth.actuallyDie();
    }

}
