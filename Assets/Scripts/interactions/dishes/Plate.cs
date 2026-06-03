using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private dishes m_dishes;

    void Start()
    {
        m_dishes.m_nbOfSpots = this.transform.childCount;
    }
}
