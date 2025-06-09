using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowPlayerX : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform m_playerTransform;

    [Header("Player x Offset")]
    [SerializeField] private float m_offset;

    [Header("Original position")]
    [SerializeField] private float m_originalXValue;

    [Header("LeanTween Speed Value")]
    [SerializeField] private float m_speed;

    [Header("Should The Camera Move With The Player")]
    public bool m_moveCam; //false = camera stays on original x value

    private void Update()
    {
        if (m_moveCam)
        {
            Vector3 desiredPosition = new Vector3(m_playerTransform.position.x + m_offset, 3.61f, 8.26f);
            Vector3 SmoothPosition = Vector3.Lerp(transform.position, desiredPosition, m_speed * Time.deltaTime);
            transform.position = SmoothPosition;
        }
        else
        {
            Vector3 SmoothPosition = Vector3.Lerp(transform.position, new Vector3(m_originalXValue, 3.61f, 8.26f), m_speed * Time.deltaTime);
            transform.position = SmoothPosition;
        }
    }

}