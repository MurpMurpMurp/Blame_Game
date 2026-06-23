using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int m_nbOfSpots;

    void Start()
    {
        m_nbOfSpots = this.transform.childCount;
    }
}
