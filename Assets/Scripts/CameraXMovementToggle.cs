using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXMovementToggle : MonoBehaviour
{
    [Header("Reference to CameraFollowPlayerX")]
    [SerializeField] private CameraFollowPlayerX m_camFollowPlayerX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cameraToggle"))
        {
            m_camFollowPlayerX.m_moveCam = !m_camFollowPlayerX.m_moveCam;
        }
    }
}
