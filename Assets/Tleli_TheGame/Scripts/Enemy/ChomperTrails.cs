using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperTrails : MonoBehaviour
{
    ParticleSystem trailsL;
    ParticleSystem trailsR;
    // Start is called before the first frame update
    void Start()
    {
        trailsR = GameObject.Find("SpikeTrailsR").GetComponent<ParticleSystem>(); // Jules
        trailsL = GameObject.Find("SpikeTrailsL").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackTrails ()
    {
        trailsL.Emit(5);
        trailsR.Emit(5);
    }
}
