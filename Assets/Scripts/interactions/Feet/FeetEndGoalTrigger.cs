using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetEndGoalTrigger : MonoBehaviour
{
    [SerializeField] private Material[] m_testMaterials;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "end goal")
        {
            MeshRenderer tempMR = other.GetComponent<MeshRenderer>();
            tempMR.material = m_testMaterials[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "end goal")
        {
            MeshRenderer tempMR = other.GetComponent<MeshRenderer>();
            tempMR.material = m_testMaterials[0];
        }
    }
}
