using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchIntensity : MonoBehaviour
{
    public float intensity;
    public float frequency;
    public Material shade1;
    public Material shade2;
    public Material shade3;
    public Material shade4;
    public Material shade5;
    public Material shade6;
    public Material shade7;
    public Material shade8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shade1.SetFloat("_intensity", intensity);
        shade2.SetFloat("_intensity", intensity);
        shade3.SetFloat("_intensity", intensity);
        shade4.SetFloat("_intensity", intensity);
        shade5.SetFloat("_intensity", intensity);
        shade6.SetFloat("_intensity", intensity);
        shade7.SetFloat("_intensity", intensity);
        shade8.SetFloat("_intensity", intensity);

        shade1.SetFloat("_frequency", frequency);
        shade2.SetFloat("_frequency", frequency);
        shade3.SetFloat("_frequency", frequency);
        shade4.SetFloat("_frequency", frequency);
        shade5.SetFloat("_frequency", frequency);
        shade6.SetFloat("_frequency", frequency);
        shade7.SetFloat("_frequency", frequency);
        shade8.SetFloat("_frequency", frequency);

    }

}
