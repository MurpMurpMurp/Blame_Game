using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXMovementToggle : MonoBehaviour
{
    [Header("Reference to CameraFollowPlayer")]
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerXNorth;
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerXWest;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cameraToggleOn"))
        {
            m_camFollowPlayerXNorth.m_moveCam = true;
            m_camFollowPlayerXWest.m_moveCam = true;
        }
        else if (other.gameObject.CompareTag("cameraToggleOff"))
        {
            m_camFollowPlayerXNorth.m_moveCam = false;
            m_camFollowPlayerXWest.m_moveCam = false;
        }
    }
}
