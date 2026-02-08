using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXMovementToggle : MonoBehaviour
{
    [Header("Reference to CameraFollowPlayer")]
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerXNorth;
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerXWest;
    [SerializeField] private GameObject m_northCamera;
    [SerializeField] private GameObject m_westCamera;

    public bool m_active;

    private void Update()
    {
        if (m_northCamera.activeSelf == true && m_westCamera.activeSelf == false)
        {
            m_active = true;
        }
        else if (m_northCamera.activeSelf == false &&  m_westCamera.activeSelf == true)
        {
            m_active = false;
        }
        else
        {
            m_active = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cameraToggleOn") && m_active)
        {
            m_camFollowPlayerXNorth.m_moveCam = true;
            m_camFollowPlayerXWest.m_moveCam = true;
        }
        else if (other.gameObject.CompareTag("cameraToggleOff") && m_active)
        {
            m_camFollowPlayerXNorth.m_moveCam = false;
            m_camFollowPlayerXWest.m_moveCam = false;
        }
    }
}
