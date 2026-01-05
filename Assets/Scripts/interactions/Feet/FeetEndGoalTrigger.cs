using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetEndGoalTrigger : MonoBehaviour
{
    [SerializeField] private Material[] m_testMaterials;

    [HideInInspector] public bool m_goalReached;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "end goal")
        {
            MeshRenderer tempMR = other.GetComponent<MeshRenderer>();
            tempMR.material = m_testMaterials[1];
            m_goalReached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "end goal")
        {
            MeshRenderer tempMR = other.GetComponent<MeshRenderer>();
            tempMR.material = m_testMaterials[0];
            m_goalReached = false;
        }
    }
}
