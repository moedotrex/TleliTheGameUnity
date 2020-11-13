using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
   /* public static ScreenShake Instance { get; private set; }
    //CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineFreeLook cinemachineFreeLook;
    float shakeTimer;

    private void Awake()
    {
        Instance = this;
        //cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = cinemachineFreeLook.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        shakeTimer = time;

    }

    private void Update()
    {
        if (shakeTimer > 0)
        { 
        shakeTimer -= Time.deltaTime;
        if (shakeTimer <= 0f)
        {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }*/
}



//CinemachineBasicMultiChannelPerlin multChanPerlin = virtualcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); and include the virtualcam.GetCinemachineComponent