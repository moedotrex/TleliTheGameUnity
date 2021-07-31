using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAnimParticles : MonoBehaviour
{
    // Heavy's particle system on swipe attacks, activated on Animation Events
    // Jules

    ParticleSystem trails; 
    ParticleSystem trailsL; 

    // Start is called before the first frame update
    void Start()
    {
        trails = GameObject.Find("ClawRTrails").GetComponent<ParticleSystem>();
        trailsL = GameObject.Find("ClawLTrails").GetComponent<ParticleSystem>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClawLParticles()
    {
        trailsL.Emit(20);
    }

    void ClawRParticles()
    {
        trails.Emit(20);
    }
}
