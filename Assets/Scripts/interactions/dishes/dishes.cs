using System.Collections;
using System.Diagnostics;
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

    [SerializeField] private GameObject[] m_platesGameObjects;
    [SerializeField] private int m_nbOfPlates;
    [SerializeField] private int m_nbOfPlatesDone;

    private RaycastHit m_hitMove;
    private GameObject m_currentPlate;

    public int m_nbOfSpots;
    public int m_nbOfSpotsDone;
    public bool m_areAllSpotsDone = false;
    public bool m_interactionFinished = false;

    private void Start()
    {
        m_nbOfPlates = m_platesGameObjects.Length;
        Plate plate = m_platesGameObjects[0].GetComponent<Plate>();
        m_nbOfSpots = plate.m_nbOfSpots;
    }

    private void Update()
    {
        if (m_nbOfSpots == m_nbOfSpotsDone)
        {
            m_nbOfPlatesDone++;
            m_nbOfSpotsDone = 0;
        }

        if (m_nbOfPlatesDone == m_nbOfPlates)
        {
            m_interactionFinished = true;
        }

        SwitchPlates();
    }

    private void SwitchPlates()
    {
        //this handles the transitions so if I switch to an animation I just need to add it here
        //also rn it only goes up to 2 plates but it's easy to add more

        switch(m_nbOfPlatesDone) 
        {
            case 0:
                break;

            case 1:
                m_platesGameObjects[0].SetActive(false);
                m_platesGameObjects[1].SetActive(true);
                break;

            default:
                break;
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
