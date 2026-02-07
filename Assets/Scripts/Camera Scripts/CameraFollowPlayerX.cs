using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowPlayerX : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private TempMovement m_movement;

    [Header("Player Offset")]
    [SerializeField] private float m_offsetX;
    [SerializeField] private float m_offsetZ;

    [Header("Original position")]
    [SerializeField] private float m_originalXValue;
    [SerializeField] private float m_originalYValue;
    [SerializeField] private float m_originalZValue;

    [Header("Desired Max And Min Clamp")]
    [SerializeField] private float m_minClampX;
    [SerializeField] private float m_maxClampX;
    [SerializeField] private float m_minClampZ;
    [SerializeField] private float m_maxClampZ;

    [Header("LeanTween Speed Value")]
    [SerializeField] private float m_speed;

    [Header("Should The Camera Move With The Player")]
    public bool m_moveCam; //false = camera stays on original x value

    [Header("Is this camera north or west")]
    [SerializeField] private bool m_cameraIsWest; // true for west camera, false for north

    private void Update()
    {
        if (!m_cameraIsWest)
        {
            if (m_moveCam)
            {
                Vector3 desiredPosition = new Vector3(Mathf.Clamp(m_playerTransform.position.x + m_offsetX, m_minClampX, m_maxClampX), m_originalYValue, m_originalZValue);
                Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, m_speed * Time.deltaTime);
                transform.position = smoothPosition;
            }
            else
            {
                Vector3 SmoothPosition = Vector3.Lerp(transform.position, new Vector3(m_originalXValue, m_originalYValue, m_originalZValue), m_speed * Time.deltaTime);
                transform.position = SmoothPosition;
            }
        }
        else
        {
            if (m_moveCam)
            {
                Vector3 desiredPosition = new Vector3(m_originalXValue, m_originalYValue, Mathf.Clamp(m_playerTransform.position.z + m_offsetZ, m_minClampZ, m_maxClampZ));
                Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, m_speed * Time.deltaTime);
                transform.position = smoothPosition;
            }
            else
            {
                Vector3 SmoothPosition = Vector3.Lerp(transform.position, new Vector3(m_originalXValue, m_originalYValue, m_originalZValue), m_speed * Time.deltaTime);
                transform.position = SmoothPosition;
            }
        }    

    }
}