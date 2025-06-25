using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXMovementToggle : MonoBehaviour
{
    [Header("Reference to CameraFollowPlayerX")]
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerX;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cameraToggleOn"))
        {
            m_camFollowPlayerX.m_moveCam = true;
        }
        else if (other.gameObject.CompareTag("cameraToggleOff"))
        {
            m_camFollowPlayerX.m_moveCam = false;
        }
    }
}
