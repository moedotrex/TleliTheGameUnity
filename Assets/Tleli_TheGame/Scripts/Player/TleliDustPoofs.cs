using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TleliDustPoofs : MonoBehaviour
{
    ParticleSystem trailsL;
    ParticleSystem trailsR;
    // Start is called before the first frame update
    void Start()
    {
        trailsR = GameObject.Find("LS_StepParticlesR").GetComponent<ParticleSystem>(); // Jules
        trailsL = GameObject.Find("LS_StepParticlesL").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RDustPoof ()
    {
        trailsR.Emit(10);
    }

    void LDustPoof ()
    {
        trailsL.Emit(10);
    }
}
