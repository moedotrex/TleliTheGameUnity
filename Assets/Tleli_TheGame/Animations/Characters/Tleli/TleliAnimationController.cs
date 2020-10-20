using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TleliAnimationController : MonoBehaviour
{
    Animator tleliAnimator;

    // Start is called before the first frame update
    void Start()
    {
        tleliAnimator = this.gameObject.GetComponent<Animator>();
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
        AnimatorStateInfo stateInfo = tleliAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("jump_fallLoop");
    }

    public void JumpFallLoopBoolParameter(bool fallLoop)
    {
        tleliAnimator.SetBool("JumpFallLoop", fallLoop);
    } 

    public void SetTleliHurtLayerWeight(float weight)
    {

        tleliAnimator.SetLayerWeight(1, weight);

    }

    public void TleliStepSound()
    {
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
         }
}
