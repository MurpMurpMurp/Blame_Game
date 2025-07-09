using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextureWithCamera : MonoBehaviour
{
    [Header("Material Reference")]
    [SerializeField] private Material m_material;

    [Header("Player Transform Reference")]
    [SerializeField] private Transform m_playerTransform;

    private float m_defaultCharacterZ;
    private float m_defaultMaterialYOffset;
    [SerializeField] private float m_differenceBetweenCharacterZAndYOffset;

    private void Start()
    {
        m_defaultCharacterZ = m_playerTransform.position.z;

        m_differenceBetweenCharacterZAndYOffset = m_defaultCharacterZ - m_defaultMaterialYOffset;
    }

    private void Update()
    {
        MakeMaterialMoveAccordingToPlayerLocation();
    }

    private void MakeMaterialMoveAccordingToPlayerLocation()
    {
        m_material.mainTextureOffset = new Vector2(0f, m_playerTransform.position.z - m_differenceBetweenCharacterZAndYOffset);
    }
}
