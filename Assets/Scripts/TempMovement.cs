using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
    [Header("Variable To Change The Forward Direction")]
    [Tooltip("change from one to four to change direction, idk which one is which but we start the game on 0 if that helps")]
    public int m_moveDir; 

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
        switch(m_moveDir) 
        {
            case 0:

                m_rb.velocity = new Vector3(m_moveVector.y * m_moveSpeed, 0, m_moveVector.x * m_moveSpeed);
                break;

            case 1:
                m_rb.velocity = new Vector3(m_moveVector.x * m_moveSpeed, 0, m_moveVector.y * m_moveSpeed);
                break;

            case 2:
                m_rb.velocity = new Vector3(-m_moveVector.y * m_moveSpeed, 0, -m_moveVector.x * m_moveSpeed);
                break;

            case 3:
                m_rb.velocity = new Vector3(-m_moveVector.x * m_moveSpeed, 0, -m_moveVector.y * m_moveSpeed);
                break;

            default:
                m_rb.velocity = new Vector3(m_moveVector.y * m_moveSpeed, 0, m_moveVector.x * m_moveSpeed);
                break;
        }
    }
}
