using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkniAnimationController : MonoBehaviour
{
    Animator ikniAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ikniAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InkingTrigger()
    {
        ikniAnimator.SetTrigger("InkingTrigger");
    }
}
