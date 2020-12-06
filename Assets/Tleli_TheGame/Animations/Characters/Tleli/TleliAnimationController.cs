﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TleliAnimationController : MonoBehaviour
{
    Animator tleliAnimator;
    LightCombo tleliCombat;
    HeavyCombo tleliHeavyCombat;
    PlayerController tleliMovement;

    // Start is called before the first frame update
    void Start()
    {
        tleliAnimator = this.gameObject.GetComponent<Animator>();
        tleliCombat = GetComponentInParent<LightCombo>();
        tleliHeavyCombat = GetComponentInParent<HeavyCombo>();
        tleliMovement = GetComponentInParent<PlayerController>();
    }

    public void SetForwardSpeedParameter(float forwardSpeed)
    {
        tleliAnimator.SetFloat("ForwardSpeed", forwardSpeed);
    }

    public void BrakeTrigger()
    {
        tleliAnimator.SetTrigger("Brake");
    }

    public void JumpTakeOffbool(bool jumpTakeOff)
    {
        tleliAnimator.SetBool("JumpTakeOff", jumpTakeOff);
    }

    public void JumpLandTrigger()
    {
        tleliAnimator.SetTrigger("JumpLand");
    }

    public void DoubleJumpTrigger()
    {
        tleliAnimator.SetTrigger("DoubleJump");
    }

    public bool CheckFallLoop()
    {
        AnimatorStateInfo stateInfo= tleliAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("jump_fallLoop");
    }

    public void JumpFallLoopBoolParameter(bool fallLoop)
    {
        tleliAnimator.SetBool("JumpFallLoop", fallLoop);
    }

    public void IsHitTrigger()
    {
        tleliAnimator.SetTrigger("IsHit");
    }

    /*public void IsDeadBool(bool isDead)
    {
           tleliAnimator.SetBool("isDead", isDead);
    }*/

    public void PauseAnims()
    {
        tleliAnimator.speed = 0;
    }

    public void ResumeAnims()
    {
        tleliAnimator.speed = 1;
    }

    public void IsDeadTrigger()
    {
        tleliAnimator.SetTrigger("IsDead");
    }

    public void DashTrigger()
    {
        tleliAnimator.SetTrigger("Dash");
    }
    public void DashPress()
    {
        tleliAnimator.SetTrigger("DashPress");
    }

    public void LightAttackTrigger()
    {
        tleliAnimator.SetTrigger("LightAttack");
    }

    public void LightAttackComboBoolParameter(bool LightAttackCombo)
    {
        tleliAnimator.SetBool("LightAttackCombo", LightAttackCombo);
    }

    public void ChargeLightAttackTigger()
    {
        tleliAnimator.SetTrigger("ChargeLightAttack");
    }

    public void ChargedUpLightAttackBoolParameter(bool ChargedUpLightAttack)
    {
        tleliAnimator.SetBool("ChargedUpLightAttack", ChargedUpLightAttack);
    }

    public void HeavyAttackTrigger()
    {
        tleliAnimator.SetTrigger("HeavyAttack");
    }

    public void HeavyAttackComboBoolParameter(bool HeavyAttackCombo)
    {
        tleliAnimator.SetBool("HeavyAttackCombo", HeavyAttackCombo);
    }

    public void light1_Forward(float duration)
    {
        tleliCombat.startAttForward(duration);
    }

    public void chargedLight_Forward(float duration)
    {
        tleliCombat.startChargedAttForward(duration);
    }

    public void LandTrigger()
    {
        tleliAnimator.SetTrigger("ChargeLightAttack");
    }


    public void lightAnimationHit()
    {
        tleliCombat.Attack();
    }

    public void heavyAnimationHit()
    {
        tleliHeavyCombat.Attack();
    }

    public void chargedLightHit()
    {
        if (tleliCombat.lunging == false)
        {
            tleliCombat.lunging = true;
        }
        else
        {
            tleliCombat.lunging = false;
        }
    }

    public void isAnimating()
    {

        if (tleliMovement.isAnimating == false)
        {
            tleliMovement.isAnimating = true;
        }
        else
        {
            tleliMovement.isAnimating = false;
        }
    }
}
