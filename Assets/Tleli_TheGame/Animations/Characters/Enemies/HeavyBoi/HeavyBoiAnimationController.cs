using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBoiAnimationController : MonoBehaviour
{
    Animator heavyBoiAnimator;
    HeavyAttack attack;
    // Start is called before the first frame update
    void Start()
    {
        heavyBoiAnimator = this.gameObject.GetComponent<Animator>();
        attack = GetComponentInParent<HeavyAttack>();
    }

    public void JumpTrigger()
    {
        heavyBoiAnimator.SetTrigger("JumpTrigger");
    }

    public void IsWalkingBoolParameter(bool IsWalking)
    {
        heavyBoiAnimator.SetBool("IsWalking", IsWalking);
    }

    public void LightAttackTrigger()
    {
        heavyBoiAnimator.SetTrigger("LightAttackTrigger");
    }

    void AttackSwing()
    {
        attack.Attack();

    }

    void AttackSmash()
    {

        attack.AttackSlam();

    }

    void turnOffAnim()
    {
        attack.isAnimating = false;
    }
}
