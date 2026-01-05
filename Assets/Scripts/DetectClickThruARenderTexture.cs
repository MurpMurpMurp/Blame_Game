using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetectClickThruARenderTexture : MonoBehaviour , IPointerClickHandler
{
    /*
     * VERY IMPORTANT!!!
     * this is the default code for interacting thru a render texture,it should not be use in the full game and can be deleted before release to lower game size
     * DO NOT MODIFY!!
    */

    [Header("References")]
    [SerializeField] private RectTransform m_rectTransform; //The rect Transform of the Raw Image that the Render Texture is on
    [SerializeField] private Camera m_thisInteractionsRenderTextureCamera; //The camera of the render texture
    [SerializeField] private LayerMask m_layer; //you must be using a layer to limit what items can be clicked on

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
            // put the code to execute upon a click on the object here
            Debug.Log("clicked on something");
        }
    }
}
