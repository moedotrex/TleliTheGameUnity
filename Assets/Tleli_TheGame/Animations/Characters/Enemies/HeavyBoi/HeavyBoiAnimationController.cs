using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBoiAnimationController : MonoBehaviour
{
    Animator heavyBoiAnimator;
    HeavyAttack attack;
    HeavyController jumpSmash;
    HeavyEnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        heavyBoiAnimator = this.gameObject.GetComponent<Animator>();
        attack = GetComponentInParent<HeavyAttack>();
        jumpSmash = GetComponentInParent<HeavyController>();
        enemyHealth = GetComponentInParent<HeavyEnemyHealth>();
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

    public void KickTrigger()
    {
        heavyBoiAnimator.SetTrigger("KickTrigger");

    }
    public void IsHitTrigger()
    {
        heavyBoiAnimator.SetTrigger("isHit");
    }

    public void IsDeadTrigger()
    {
        heavyBoiAnimator.SetTrigger("isDeadT");
    }

    void AttackSwing()
    {
        attack.Attack();

    }

    void AttackSmash()
    {

        attack.AttackSlam();

    }

    void JumpEnd()
    {

        jumpSmash.JumpAnimOff();
        attack.JumpSlamp();

    }

    void turnOffAnim()
    {
        attack.isAnimating = false;
    }

    void jumpEndOnly()
    {
        jumpSmash.JumpAnimOff();
    }

    public void Die()
    {
        enemyHealth.actuallyDie();
    }
}
