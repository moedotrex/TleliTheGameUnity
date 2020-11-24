using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBoiAnimationController : MonoBehaviour
{
    Animator heavyBoiAnimator;

    // Start is called before the first frame update
    void Start()
    {
        heavyBoiAnimator = this.gameObject.GetComponent<Animator>();
    }

    public void JumpTrigger()
    {
        heavyBoiAnimator.SetTrigger("JumpTrigger");
    }

    public void IsWalkingBoolParameter(bool IsWalking)
    {
        heavyBoiAnimator.SetBool("IsWalking", IsWalking);
    }
}
