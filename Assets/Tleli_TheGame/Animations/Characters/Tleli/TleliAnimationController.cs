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
}
