using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NpcCameraController : MonoBehaviour
{
    CinemachineFreeLook cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cam = GameObject.FindGameObjectWithTag("CameraBrain").GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartedDialogue()
    {
        Cursor.visible = true;
        Debug.Log("Successfully entered dialogue and ran the StartedDialogue method");
        cam.m_YAxis.m_MaxSpeed = 0;
        cam.m_XAxis.m_MaxSpeed = 0;
    }

    public void EndedDialogue()
    {
        Cursor.visible = false;
        Debug.Log("Successfully exited dialogue and ran the EndedDialogue method");
        cam.m_YAxis.m_MaxSpeed = 1.5f;
        cam.m_XAxis.m_MaxSpeed = 350;
    }
}
