using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTest : MonoBehaviour
{
    [SerializeField] private bool m_flopper = false;
    [SerializeField] private Animator m_animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_flopper)
            {
                m_animator.SetTrigger("Go up");
                m_flopper=true;
            }
            else 
            {
                m_animator.SetTrigger("Go down");
                m_flopper = false;
            }
        }
    }
}
