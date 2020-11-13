using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivity : MonoBehaviour
{
    
    public float cameraSensitivity = 2f;
    CinemachineFreeLook freeLook;
    private float ySensitivity;
    private float xSensitivity; 

    public void Start()
    {
        
        freeLook = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
        ySensitivity = 1.5f * cameraSensitivity;
        xSensitivity = 350f * cameraSensitivity;
        SetCameraSensitivity();
}

    public void SetCameraSensitivity()
    {
        freeLook.m_XAxis= new AxisState(0.5f, ySensitivity, false, true, 0.1f, 0.1f, 0, "Mouse Y", false);
        freeLook.m_XAxis = new AxisState(-180, 180, true, false, xSensitivity, 0.1f, 0.1f, "Mouse X", false);
    }

    
}
