using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingWalls : MonoBehaviour, IEquatable<FadingWalls>
{
    public List<Renderer> m_renderers = new List<Renderer>();
    public Vector3 m_position;
    public List<Material> m_materials = new List<Material>();
    [HideInInspector] public float m_initialAlpha;

    private void Awake()
    {
        m_position = transform.position;

        if (m_renderers.Count == 0)
        {
            m_renderers.AddRange(GetComponentsInChildren<Renderer>());
        }
        foreach(Renderer renderer in m_renderers)
        {
            m_materials.AddRange(renderer.materials);
        }

        m_initialAlpha = m_materials[0].color.a;
    }

    public bool Equals(FadingWalls other)
    {
        return m_position.Equals(other.m_position);
    }

    public override int GetHashCode()
    {
        return m_position.GetHashCode();
    }
}
