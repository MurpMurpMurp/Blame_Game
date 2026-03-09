using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class lookOutside : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler
{
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera;
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private Transform m_centerObject;
    [SerializeField] private GameObject m_midPointObject;
    [SerializeField] private Rigidbody m_draggingBehindObject;
    [SerializeField] private float m_force;

    [SerializeField] private float m_distanceToLookAt;

    private Vector2 m_mousePosition;
    private RaycastHit m_hitMove;
    private RaycastHit m_hitDown;

    private Vector3 m_pointToLookAt;

    //ok so hear me out, me, what if we had an invisible object dragging behind the mouse and that was what the camera looked at and that's what was clamped
    //ok so nevermind that previous idea, what if I had an object that was always centered and I looked at a point that was somewhere in between the points

    private void Update()
    {
        GetTheInBetweenPoint();
        MakeCameraLagBehind();
    }

    private void GetTheInBetweenPoint()
    {
        m_pointToLookAt = (m_hitMove.point - m_centerObject.position).normalized;
        float distance = Vector3.Distance(m_hitMove.point, m_centerObject.position);
        m_midPointObject.transform.position = m_centerObject.position + ((distance * m_distanceToLookAt) * m_pointToLookAt);
        m_midPointObject.transform.localPosition = new Vector3(m_midPointObject.transform.localPosition.x, m_midPointObject.transform.localPosition.y, Mathf.Clamp(m_midPointObject.transform.localPosition.z, 15.42956f, 16.74f));
    }

    private void MakeCameraLagBehind()
    {
        if (Vector3.Distance(m_draggingBehindObject.position, m_midPointObject.transform.position) >= 0.15f)
        {
            m_draggingBehindObject.AddForce((m_midPointObject.transform.position - m_draggingBehindObject.position).normalized * m_force, ForceMode.Force);
        }
        m_thisInteractionsRenderTextureCamera.transform.LookAt(m_draggingBehindObject.transform.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, eventData.position, null, out Vector2 localClick);

        localClick.x = (m_rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (m_rectTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / m_rectTransform.rect.size.x, localClick.y / m_rectTransform.rect.size.y);

        Ray ray = m_thisInteractionsRenderTextureCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out m_hitDown, Mathf.Infinity, m_layer))
        {

        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, eventData.position, null, out Vector2 localClick);

        localClick.x = (m_rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (m_rectTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / m_rectTransform.rect.size.x, localClick.y / m_rectTransform.rect.size.y);

        Ray ray = m_thisInteractionsRenderTextureCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out m_hitMove, 20f, m_layer))
        {
            
        }
    }
}
