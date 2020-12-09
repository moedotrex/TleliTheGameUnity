using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorAnimationController : MonoBehaviour
{
    Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void BossKeysTrigger()
    {
        doorAnimator.SetTrigger("GotBossKeys");
    }
}
