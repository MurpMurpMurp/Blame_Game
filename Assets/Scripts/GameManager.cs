using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is where I manage the game and all the triggers

    [Header("Switch Camera Track")]
    [SerializeField] private bool m_followNorthTrack = true;
    [SerializeField] private GameObject m_northVirtualCamera;
    [SerializeField] private GameObject m_westVirtualCamera;

    private bool m_flopper;

    private void Start()
    {
        if (m_followNorthTrack)
        {
            m_northVirtualCamera.SetActive(true);
            m_westVirtualCamera.SetActive(false);
        }
        else
        {
            m_northVirtualCamera.SetActive(false);
            m_westVirtualCamera.SetActive(true);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_flopper = true;
        }

        SwitchBetweenNorthAndWestTracks();
    }

    private void SwitchBetweenNorthAndWestTracks()
    {
        if (m_followNorthTrack && m_flopper) //switches from north to west
        {
            m_followNorthTrack = !m_followNorthTrack;
            m_westVirtualCamera.SetActive(true);
            m_northVirtualCamera.SetActive(false);
            m_flopper = false;
            Debug.Log("north to west");
        }
        else if (!m_followNorthTrack && m_flopper) //switches from west to north 
        {
            m_followNorthTrack = !m_followNorthTrack;
            m_northVirtualCamera.SetActive(true);
            m_westVirtualCamera.SetActive(false);
            m_flopper = false;
            Debug.Log("west to north");
        }
    }
}
