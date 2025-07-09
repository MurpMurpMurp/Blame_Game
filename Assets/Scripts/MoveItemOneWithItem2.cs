using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemOneWithItem2 : MonoBehaviour
{
    [Header("Items Reference")]
    [SerializeField] private Transform m_item1Transform;
    [SerializeField] private Transform m_item2Transform;

    private void Update()
    {
        m_item1Transform.SetPositionAndRotation(m_item2Transform.position, m_item2Transform.rotation);
    }
}
