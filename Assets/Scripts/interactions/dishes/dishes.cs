using UnityEngine;
using UnityEngine.EventSystems;

public class dishes : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera;
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private GameObject m_test;
    [SerializeField] private float m_damage;

    [SerializeField] private Plate m_plate;

    private RaycastHit m_hitMove;

    public int m_nbOfSpots;
    public int m_nbOfSpotsDone;
    public bool m_areAllSpotsDone = false;

    void Update()
    {
        if (m_nbOfSpots == m_nbOfSpotsDone)
        {
            m_areAllSpotsDone = true;
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
            if (m_hitMove.collider.gameObject.CompareTag("spot"))
            {
                m_hitMove.collider.gameObject.GetComponent<spots>().m_health -= m_damage;

                if (m_hitMove.collider.gameObject.GetComponent<spots>().m_isThisSpotDone == true)
                {
                    m_nbOfSpotsDone++;
                    m_hitMove.collider.gameObject.SetActive(false);
                }
            }
        }

        m_test.transform.position = m_hitMove.point;
    }
}
