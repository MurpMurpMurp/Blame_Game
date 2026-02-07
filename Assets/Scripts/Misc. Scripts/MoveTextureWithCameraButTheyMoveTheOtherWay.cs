using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextureWithCameraButTheyMoveTheOtherWay : MonoBehaviour
{
    [Header("Material Reference")]
    [SerializeField] private Material m_material;

    [Header("Player Transform Reference")]
    [SerializeField] private Transform m_playerTransform;

    [Header("Texture Move Speed")]
    [SerializeField] private float m_speed;

    private float m_defaultCharacterZ;
    private float m_defaultMaterialXOffset;
    [SerializeField] private float m_differenceBetweenCharacterZAndXOffset;

    private void Start()
    {
        m_defaultCharacterZ = m_playerTransform.position.z;

        m_differenceBetweenCharacterZAndXOffset = m_defaultCharacterZ - m_defaultMaterialXOffset;
    }

    private void Update()
    {
        MakeMaterialMoveAccordingToPlayerLocation();
    }

    private void MakeMaterialMoveAccordingToPlayerLocation()
    {
        m_material.mainTextureOffset = new Vector2(((m_playerTransform.position.z - m_differenceBetweenCharacterZAndXOffset) / m_speed) * -1, 0f);
    }
}
