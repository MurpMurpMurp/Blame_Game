using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampObjectToOnlyMoveInXDirection : MonoBehaviour
{
    [Header("Y position Reference")]
    [SerializeField] private float m_Y;

    private float m_defaultYPosition;

    private void Start()
    {
        m_defaultYPosition = this.transform.position.y;
    }

    private void Update()
    {
        ClampTheThingsToClamp(); 
    }

    private void ClampTheThingsToClamp()
    {
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, m_defaultYPosition, m_defaultYPosition), this.transform.position.z);
    }
}
