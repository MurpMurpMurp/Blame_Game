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
    [SerializeField] private Button m_lowerInteractionScreenButton;

    [Header("Animator References")]
    [SerializeField] private Animator m_feetAnimator;

    [Header("test Stuff")]
    [SerializeField] private GameObject m_gameObject;

    private bool m_playerHasClickedOnScreenToLowerInteraction = false;

    private bool m_feetIsUp = false;

    private void Start()
    {
        if (m_rectTransform == null)
        {
            m_rectTransform = this.GetComponent<RectTransform>();
        }
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
            m_gameObject.transform.position = hit.point;

            if (hit.collider.gameObject.tag == "feet" && Vector3.Distance(m_player.position, hit.collider.gameObject.transform.position) <= m_distanceForInteraction)
            {
                m_feetAnimator.SetTrigger("Go up");
                m_feetIsUp = true;
                m_tempMovement.m_canPlayerMove = false;
                m_lowerInteractionScreenButton.enabled = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || m_playerHasClickedOnScreenToLowerInteraction)
        {
            if (m_feetIsUp)
            {
                m_feetAnimator.SetTrigger("Go down");
                m_tempMovement.m_canPlayerMove = true;
            }

            m_lowerInteractionScreenButton.enabled = false;
            m_playerHasClickedOnScreenToLowerInteraction = false;
        }
    }

    public void LowerInteractionScreen()
    {
        m_playerHasClickedOnScreenToLowerInteraction = true;
        Debug.Log("button has been pressed");
    }
}
