using UnityEngine;

public class spots : MonoBehaviour
{
    [Header("Health")]
    public float m_health;
    private int m_maxHealth;


    private Vector3 m_initialSize;

    public bool m_isThisSpotDone = false;


    void Start()
    {
        m_initialSize = transform.localScale;
        m_maxHealth = (int)m_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isThisSpotDone)
        {
            this.transform.localScale = new Vector3(m_initialSize.x * m_health / m_maxHealth, m_initialSize.y * m_health / m_maxHealth, m_initialSize.z * m_health / m_maxHealth);
        }

        if (m_health < 40 && !m_isThisSpotDone)
        {
            m_isThisSpotDone = true;
        }
    }
}
