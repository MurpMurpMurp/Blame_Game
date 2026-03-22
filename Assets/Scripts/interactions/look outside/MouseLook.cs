using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Directions")]
    [SerializeField] private float m_mouseX;
    [SerializeField] private float m_mouseY;

    [Header("Mouse Variables")]
    [SerializeField] private float m_sensitivity = 100f;
    [SerializeField] private Slider m_sensitivitySlider;

    [Header("Max Rotation")]
    [SerializeField] private float m_maxXValue;
    [SerializeField] private float m_minXValue;
    [SerializeField] private float m_maxYValue;
    [SerializeField] private float m_minYValue;

    private float m_xRotation = 0f;
    private float m_yRotation = 0f;

    public bool m_canMove = true;

    private void Update()
    {
        UpdateMouseDirection();
        RotateBody();
    }

    private void UpdateMouseDirection()
    {
        m_mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
        m_mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

        m_xRotation -= m_mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, m_minXValue, m_maxXValue);

        m_yRotation += m_mouseX;
        m_yRotation = Mathf.Clamp(m_yRotation, m_minYValue, m_maxYValue);
    }

    private void RotateBody()
    {
        transform.localRotation = Quaternion.Euler(m_xRotation, m_yRotation, 0f);
    }

    public void SetSensitivity()
    {
        m_sensitivity = m_sensitivitySlider.value;
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }
}
