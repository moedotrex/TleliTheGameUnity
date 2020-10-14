using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Karime
// Hace que el objeto quede de frente a la cámara después de todos los movimientos hechos en Update()
public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;

   
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}