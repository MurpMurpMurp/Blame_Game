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

    [Header("Original x value")]
    [SerializeField] private float m_originalXValue;

    [Header("LeanTween Speed Value")]
    [SerializeField] private float m_speed;

    [Header("Should The Camera Move With The Player")]
    [SerializeField] private bool m_moveCam; //false = camera stays on original x value

    private void Start()
    {
        LeanTween.init(800);
        m_originalXValue = this.gameObject.transform.localPosition.x;
        m_offset = Mathf.Abs(m_originalXValue - m_playerTransform.position.x);
    }

    private void Update()
    {
        if (m_moveCam && !LeanTween.isTweening(this.gameObject))
        {
            LeanTween.moveLocalX(this.gameObject, m_playerTransform.position.x + m_offset, m_speed);
        }
        else
        {
            LeanTween.moveLocalX(this.gameObject, m_originalXValue, m_speed);
        }
    }

}
