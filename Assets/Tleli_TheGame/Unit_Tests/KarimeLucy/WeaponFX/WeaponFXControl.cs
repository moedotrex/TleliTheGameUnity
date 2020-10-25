using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

//Karime y Lucy. Controla colores y atributos de los efectos del arma

public class WeaponFXControl : MonoBehaviour
{
    public ParticleSystem slashPS;
    public ParticleSystem particlesPS;
    public ParticleSystem trailsPS;
    public VisualEffect sparksVFX;

    [ColorUsageAttribute(true, true)]
    public Color slashEmissionColor;
    public Color slashBaseColor;

    public Gradient particleColorOverLifetime;
    public Gradient trailColorOverLifetime;

    public Gradient sparksColor;
    public Gradient movingParticlesColor;
    public Gradient movingTrailsColor;

    Material slashMat;

    void Start()
    {
        slashPS = GameObject.Find("WeaponSlash").GetComponent<ParticleSystem>();
        particlesPS = GameObject.Find("WeaponParticles").GetComponent<ParticleSystem>();
        trailsPS = GameObject.Find("WeaponTrails").GetComponent<ParticleSystem>();
        sparksVFX = GameObject.Find("SwordSparks").GetComponent<VisualEffect>();

        //SLASH

        slashMat = slashPS.GetComponent<ParticleSystemRenderer>().material;
        slashMat.SetColor("_BaseColor", slashBaseColor);
        slashMat.SetColor("_EmissionColor", slashEmissionColor);

        //PARTICLES

        Gradient particleGradient = new Gradient();
        var partCol = particlesPS.colorOverLifetime;
        partCol.enabled = true;
        partCol.color = particleColorOverLifetime;

        //TRAILS

        Gradient trailGradient = new Gradient();
        var trailCol = trailsPS.colorOverLifetime;
        trailCol.enabled = true;
        trailCol.color = trailColorOverLifetime;

        //VFX (Sparks & Rotating Particles + Trails)

        sparksVFX.SetGradient("SparkColor", sparksColor);
        sparksVFX.SetGradient("MovingParticleColor", movingParticlesColor);
        sparksVFX.SetGradient("TrailColor", movingTrailsColor);
    }

}

/*  
    Gradient particleGradient = new Gradient();
    particleGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(particleColor, 0.5f)} , new GradientAlphaKey[] { new GradientAlphaKey(0f, 0.5f), new GradientAlphaKey(0.5f, 1f), new GradientAlphaKey(0f, 1f) });
    var col = particlesPS.colorOverLifetime;
    col.enabled = true;
    col.color = particleGradient;
  */
