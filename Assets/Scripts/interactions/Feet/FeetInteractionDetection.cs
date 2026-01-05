using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeetInteractionDetection : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
{
    [Header("References")]
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera;
    [SerializeField] private LayerMask m_layer;

    [Header("Feet Interaction Specific Stuff")]
    [SerializeField] private bool m_mocassinWasPressed;
    [SerializeField] private Rigidbody m_hitRigidbody;
    [SerializeField] private float m_force = 1;

    [Header("test")]
    [SerializeField] private GameObject m_testObject;

    private RaycastHit m_hitDown;
    private RaycastHit m_hitMove;

    private void Start()
    {
        if (m_rectTransform == null)
        {
            m_rectTransform = this.GetComponent<RectTransform>();
        }

        m_hitRigidbody = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_mocassinWasPressed = false;
        }
    }

    private void FixedUpdate()
    {
        MoveTheObjectThatWasClickedOn();
    }

    private void MoveTheObjectThatWasClickedOn()
    {
        if (m_hitRigidbody != null && m_mocassinWasPressed)
        {
            Debug.Log("holy crap lois I'm in the debugger");

            m_hitRigidbody.AddForce((m_hitMove.point - m_hitRigidbody.position).normalized * m_force, ForceMode.Force);
        }
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
            if (m_hitDown.collider.gameObject.tag == "feet")
            {
                m_hitRigidbody = m_hitDown.rigidbody;
                m_mocassinWasPressed = true;
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, eventData.position, null, out Vector2 localClick);

        localClick.x = (m_rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (m_rectTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / m_rectTransform.rect.size.x, localClick.y / m_rectTransform.rect.size.y);

        Ray ray = m_thisInteractionsRenderTextureCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out m_hitMove, Mathf.Infinity, m_layer))
        {

        }
        m_testObject.transform.position = m_hitMove.point;
    }
}
