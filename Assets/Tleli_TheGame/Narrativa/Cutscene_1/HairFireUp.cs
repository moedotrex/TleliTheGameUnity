using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairFireUp : MonoBehaviour
{
    public float power;
    public Material fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fire.SetFloat("Vector1_7D02F6C0", power);
    }
}
