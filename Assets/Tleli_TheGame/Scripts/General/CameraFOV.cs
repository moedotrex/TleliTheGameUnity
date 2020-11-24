using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    CinemachineFreeLook freeLook;
    TlelliFlameHealth tlelliFlameHealth;
    public int battleFieldOfView = 115;
    public int defaultFieldOfView = 70;
    // Start is called before the first frame update
    void Start()
    {
        freeLook = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
        tlelliFlameHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();

    }


    
    void Update()
    {
        print(tlelliFlameHealth.isBattling);
        switch (tlelliFlameHealth.isBattling)
        {
            case true:
                if (freeLook.m_Lens.FieldOfView != battleFieldOfView)
                {
                    freeLook.m_Lens.FieldOfView = Mathf.Lerp(freeLook.m_Lens.FieldOfView, battleFieldOfView, Time.deltaTime / 2);
                }
                else freeLook.m_Lens.FieldOfView = battleFieldOfView;

                break;

            case false:
                if (freeLook.m_Lens.FieldOfView != defaultFieldOfView)
                {
                    freeLook.m_Lens.FieldOfView = Mathf.Lerp(freeLook.m_Lens.FieldOfView, defaultFieldOfView, Time.deltaTime / 2);
                }
                else freeLook.m_Lens.FieldOfView = defaultFieldOfView;
                break;
        }
    }
}




