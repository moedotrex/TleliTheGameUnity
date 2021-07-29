using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamLockTest : MonoBehaviour
{
    CinemachineFreeLook cam;
    public LightCombo lightAttack;
    public HeavyCombo heavyAttack;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
        lightAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<LightCombo>();
        heavyAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<HeavyCombo>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //Debug.Log("Successfully entered dialogue and ran the StartedDialogue method");
            cam.m_YAxis.m_MaxSpeed = 0;
            cam.m_XAxis.m_MaxSpeed = 0;
            lightAttack.enabled = false;
            heavyAttack.enabled = false;
            player.enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.J))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Debug.Log("Successfully exited dialogue and ran the EndedDialogue method");
            cam.m_YAxis.m_MaxSpeed = 1.5f;
            cam.m_XAxis.m_MaxSpeed = 350;
            lightAttack.enabled = true;
            heavyAttack.enabled = true;
            player.enabled = true;
        }

        else if (Input.GetKeyDown(KeyCode.I))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //Debug.Log("Successfully entered dialogue and ran the StartedDialogue method");
            cam.m_YAxis.m_InputAxisName = "";
            cam.m_XAxis.m_InputAxisName = "";
            lightAttack.enabled = false;
            heavyAttack.enabled = false;
            player.enabled = false;

        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Debug.Log("Successfully exited dialogue and ran the EndedDialogue method");
            cam.m_YAxis.m_InputAxisName = "Mouse Y";
            cam.m_XAxis.m_InputAxisName = "Mouse X";
            lightAttack.enabled = true;
            heavyAttack.enabled = true;
            player.enabled = true;
        }


    }
}
