using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;

public class dishes : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera;
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private GameObject m_test;
    [SerializeField] private float m_damage;

    private RaycastHit m_hitMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            if (m_hitMove.collider.gameObject.CompareTag("spot"))
            {
                m_hitMove.collider.gameObject.GetComponent<spots>().m_health -= m_damage;
            }
        }

        m_test.transform.position = m_hitMove.point;
    }
}
