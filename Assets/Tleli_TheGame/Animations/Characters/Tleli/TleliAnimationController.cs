using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TleliAnimationController : MonoBehaviour
{
    Animator tleliAnimator;
    LightCombo tleliCombat;

    // Start is called before the first frame update
    void Start()
    {
        tleliAnimator = this.gameObject.GetComponent<Animator>();
        tleliCombat = GetComponentInParent<LightCombo>();
    }

    public void SetForwardSpeedParameter(float forwardSpeed)
    {
        tleliAnimator.SetFloat("ForwardSpeed", forwardSpeed);
    }

    public void JumpTakeOffTrigger()
    {
        tleliAnimator.SetTrigger("JumpTakeOff");
    }

    public void JumpLandTrigger()
    {
        tleliAnimator.SetTrigger("JumpLand");
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

    public void IsDeadTrigger()
    {
        
        tleliAnimator.SetTrigger("Death");
    }
    public void DashTrigger()
    {
        tleliAnimator.SetTrigger("Dash");
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

    public void lightAnimationHit()
    {
        tleliCombat.Attack();
    }
    public void chargedLightHit()
    {
        if (tleliCombat.lunging == false)
        {
            tleliCombat.lunging = true;
            tleliCombat.FaceTarget();
        }

        else if (tleliCombat.lunging)
        {
            tleliCombat.lunging = false;
        }
    }
}
