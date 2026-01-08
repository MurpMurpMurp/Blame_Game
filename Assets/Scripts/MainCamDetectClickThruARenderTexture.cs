using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCamDetectClickThruARenderTexture : MonoBehaviour , IPointerClickHandler
{
    [Header("Variables")]
    [SerializeField] private float m_distanceForInteraction;

    [Header("References")]
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private ScreenScriptableObjects m_screenScriptableObjects;
    [SerializeField] private Transform m_player;
    [SerializeField] private TempMovement m_tempMovement;
    

    [Header("Feet Interaction References")]
    [SerializeField] private FeetInteractionDetection m_feetInteractionDetection;
    [SerializeField] private Animator m_feetAnimator;
    [SerializeField] private Camera m_feetCamera;

    [Header("Time Before Lowering Interaction Upon Completion")]
    [SerializeField] private float m_timer;
    [SerializeField] private float m_timeToReach;
    [SerializeField] private float m_timeToReach2;
    [SerializeField] private bool m_timerDone;
    

    //[Header("test Stuff")]
    //[SerializeField] private GameObject m_gameObject;


    private bool m_feetIsUp = false;

    private void Start()
    {
        if (m_rectTransform == null)
        {
            m_rectTransform = this.GetComponent<RectTransform>();
        }

        // turn off interaction cameras until needed
        m_feetCamera.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, eventData.position, null, out Vector2 localClick);

        localClick.x = (m_rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (m_rectTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / m_rectTransform.rect.size.x, localClick.y / m_rectTransform.rect.size.y);

        Ray ray = m_thisInteractionsRenderTextureCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_layer))
        {
            if (hit.collider.gameObject.tag == "feet" && Vector3.Distance(m_player.position, hit.collider.gameObject.transform.position) <= m_distanceForInteraction)
            {
                m_feetAnimator.SetTrigger("Go up");
                m_feetIsUp = true;
                m_tempMovement.m_canPlayerMove = false;
                m_timer = 0;
                m_timerDone = false;
                m_feetCamera.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (m_feetInteractionDetection.m_feetCompleted)
        {
            if (m_timer < m_timeToReach)
            {
                m_timerDone = false;
            }
            else
            {
                m_timerDone = true;
            }
            if (m_timer < m_timeToReach2)
            {
                m_timer += Time.deltaTime;
            }
            else
            {
                m_feetCamera.gameObject.SetActive(false);
            }

            if (m_feetIsUp && m_timerDone)
            {
                m_feetAnimator.SetTrigger("Go down");
                m_tempMovement.m_canPlayerMove = true;
            }
        }
    }
}
