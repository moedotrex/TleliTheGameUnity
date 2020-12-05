using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivity : MonoBehaviour
{
    
    public float cameraSensitivity;
    CinemachineFreeLook freeLook;
    private float ySensitivity;
    private float xSensitivity; 

    public void Start()
    {

        cameraSensitivity = PlayerPrefs.GetFloat("CameraSensitivity", 1f);

        freeLook = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
        SetCameraSensitivity(cameraSensitivity);
}

    public void SetCameraSensitivity(float sensitivity)
    {
        ySensitivity = 1.5f * sensitivity;
        xSensitivity = 350f * sensitivity;

        freeLook.m_XAxis= new AxisState(0.5f, ySensitivity, false, true, 0.1f, 0.1f, 0, "Mouse Y", false);
        freeLook.m_XAxis = new AxisState(-180, 180, true, false, xSensitivity, 0.1f, 0.1f, "Mouse X", false);
    }

    
}
