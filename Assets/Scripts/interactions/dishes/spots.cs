using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spots : MonoBehaviour
{
    [Header("Health")]
    public float m_health;
    private int m_maxHealth;


    private Vector3 m_initialSize;


    void Start()
    {
        m_initialSize = transform.localScale;
        m_maxHealth = (int)m_health;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(m_initialSize.x * m_health / m_maxHealth, m_initialSize.y * m_health / m_maxHealth, m_initialSize.z * m_health / m_maxHealth);

        //if 
    }
}
