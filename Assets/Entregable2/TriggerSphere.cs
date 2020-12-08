using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : MonoBehaviour
{
    public Material TriggerMaterial;

    public float trigger;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        trigger = Mathf.Lerp(trigger, 0, Time.deltaTime);

        TriggerMaterial.SetFloat("_trigger", trigger);

        if (Input.GetButtonDown("Jump"))
        {
            trigger += 1f;
        }
    }
}
