using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectsBlockingObject : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private Transform m_target;
    [SerializeField] private Camera m_camera;
    [SerializeField][Range(0, 1f)] private float m_fadedAlpha;
    [SerializeField] bool m_retainShadows = true;
    [SerializeField] Vector3 m_targetPositionOffset = Vector3.up;
    [SerializeField] float m_fadeSpeed = 1;

    [Header("Read Only Data")]
    [SerializeField] private List<FadingWalls> m_wallsBlockingView = new List<FadingWalls>();
    private Dictionary<FadingWalls, Coroutine> m_runningCoroutines = new Dictionary<FadingWalls, Coroutine>();

    private RaycastHit[] m_hits = new RaycastHit[10];

    private void Start()
    {
        StartCoroutine(CheckForObjects());
    }

    private IEnumerator CheckForObjects()
    {
        while (true) 
        {
            int hits = Physics.RaycastNonAlloc(
                m_camera.transform.position,
                (m_target.transform.position + m_targetPositionOffset - m_camera.transform.position).normalized,
                m_hits,
                Vector3.Distance(m_camera.transform.position, m_target.transform.position + m_targetPositionOffset),
                m_layerMask
                );

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    FadingWalls fadingWalls = GetFadingObjectFromHit(m_hits[i]);

                    if (fadingWalls != null && ! m_wallsBlockingView.Contains(fadingWalls))
                    {
                        if (m_runningCoroutines.ContainsKey(fadingWalls))
                        {
                            if (m_runningCoroutines[fadingWalls] != null)
                            {
                                StopCoroutine(m_runningCoroutines[fadingWalls]);
                            }
                            m_runningCoroutines.Remove(fadingWalls);
                        }

                        m_runningCoroutines.Add(fadingWalls, StartCoroutine(FadeObjectOut(fadingWalls)));
                        m_wallsBlockingView.Add(fadingWalls);

                    }
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return null;
        }
    }

    private void FadeObjectsNoLongerBeingHit()
    {
        List<FadingWalls> objectsToRemove = new List<FadingWalls>(m_wallsBlockingView.Count);

        foreach(FadingWalls fadingWall in m_wallsBlockingView)
        {
            bool objectIsBeingHit = false;
            for (int i = 0; i < m_hits.Length; i++)
            {
                FadingWalls hitFadingObject = GetFadingObjectFromHit(m_hits[i]);
                if (hitFadingObject != null && fadingWall == hitFadingObject)
                {
                    objectIsBeingHit = true;
                    break;
                }
            }
            if (!objectIsBeingHit)
            {
                if (m_runningCoroutines.ContainsKey(fadingWall))
                {
                    if (m_runningCoroutines[fadingWall] != null)
                    {
                        StopCoroutine(m_runningCoroutines[fadingWall]);
                    }
                    m_runningCoroutines.Remove(fadingWall);
                }
                
                m_runningCoroutines.Add(fadingWall, StartCoroutine(FadeObjectIn(fadingWall)));
                objectsToRemove.Add(fadingWall);
            }
        }

        foreach(FadingWalls removeObject in objectsToRemove)
        {
            m_wallsBlockingView.Remove(removeObject);
        }
    }

    private IEnumerator FadeObjectOut(FadingWalls FadingWall)
    {
        foreach (Material material in FadingWall.m_materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcColor);
            material.SetInt("_ZWrite", 0);
            material.SetInt("_Surface", 1);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            material.SetShaderPassEnabled("DepthOnly", false);
            material.SetShaderPassEnabled("SHADOWCASTER", m_retainShadows);

            material.SetOverrideTag("RenderType", "Transparent");

            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        float time = 0;

        while (FadingWall.m_materials[0].color.a > m_fadedAlpha)
        {
            foreach (Material material in FadingWall.m_materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(FadingWall.m_initialAlpha, m_fadedAlpha, time * m_fadeSpeed)
                        );
                }
            }

            time += Time.deltaTime;
            yield return null;

        }

        if (m_runningCoroutines.ContainsKey(FadingWall))
        {
            StopCoroutine(m_runningCoroutines[FadingWall]);
            m_runningCoroutines.Remove(FadingWall);
        }
    }

    private IEnumerator FadeObjectIn(FadingWalls FadingWall)
    {
        float time = 0;

        while (FadingWall.m_materials[0].color.a < FadingWall.m_initialAlpha)
        {
            foreach (Material material in FadingWall.m_materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(m_fadedAlpha, FadingWall.m_initialAlpha, time * m_fadeSpeed)
                        );
                }
            }

            time += Time.deltaTime;
            yield return null;

        }

        foreach (Material material in FadingWall.m_materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.SetInt("_Surface", 0);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            material.SetShaderPassEnabled("DepthOnly", true);
            material.SetShaderPassEnabled("SHADOWCASTER", true);

            material.SetOverrideTag("RenderType", "Opaque");

            material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        if (m_runningCoroutines.ContainsKey(FadingWall))
        {
            StopCoroutine(m_runningCoroutines[FadingWall]);
            m_runningCoroutines.Remove(FadingWall);
        }
    }

    private FadingWalls GetFadingObjectFromHit(RaycastHit Hit)
    {
        return Hit.collider != null ? Hit.collider.GetComponent<FadingWalls>() : null;
    }

    private void ClearHits()
    {
        System.Array.Clear(m_hits, 0, m_hits.Length);
    }

}
