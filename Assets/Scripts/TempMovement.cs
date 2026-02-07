using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_rotateSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] public Camera m_camera;

    [Header("others")]
    [SerializeField] private Vector2 m_input;

    public bool m_canPlayerMove = true;

    


    private void Start()
    {
        var forward = m_camera.transform.forward;
        var right = m_camera.transform.right;
    }

    private void Update()
    {
        if (m_canPlayerMove)
        {
            ProcessInputs();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        m_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_input = Vector2.ClampMagnitude(m_input, 1);
    }

    private void Move()
    {
        Vector3 camF = m_camera.transform.forward;
        Vector3 camR = m_camera.transform.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        m_rb.transform.position += (camF * m_input.y + camR * m_input.x) * m_moveSpeed * Time.deltaTime;
    }
}
