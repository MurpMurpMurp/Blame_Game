using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float m_moveSpeed;

    [Header("References")]
    [SerializeField] Rigidbody m_rb;

    private Vector3 m_moveVector;

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical") * -1;

        m_moveVector = new Vector3(moveX, moveY, 0).normalized;
    }

    private void Move()
    {
        m_rb.velocity = new Vector3(m_moveVector.y * m_moveSpeed, 0, m_moveVector.x * m_moveSpeed);
    }
}
