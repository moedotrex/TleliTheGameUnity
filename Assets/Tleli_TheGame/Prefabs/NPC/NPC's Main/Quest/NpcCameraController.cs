using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NpcCameraController : MonoBehaviour
{
    CinemachineFreeLook cam;
    public LightCombo lightAttack;
    public HeavyCombo heavyAttack;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cam = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
        lightAttack = GameObject.Find("Player").GetComponent<LightCombo>();
        heavyAttack = GameObject.Find("Player").GetComponent<HeavyCombo>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartedDialogue()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Successfully entered dialogue and ran the StartedDialogue method");
        cam.m_YAxis.m_MaxSpeed = 0;
        cam.m_XAxis.m_MaxSpeed = 0;
        lightAttack.enabled = false;
        heavyAttack.enabled = false;
        player.enabled = false;
        
    }

    public void EndedDialogue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Successfully exited dialogue and ran the EndedDialogue method");
        cam.m_YAxis.m_MaxSpeed = 1.5f;
        cam.m_XAxis.m_MaxSpeed = 350;
        lightAttack.enabled = true;
        heavyAttack.enabled = true;
        player.enabled = true;
    }
}
