using System.Collections;
using UnityEngine;

public class wallsGoAwayWhenHidePlayer : MonoBehaviour
{
    //[SerializeField] private MeshRenderer[] m_thingsToRemove;
    [SerializeField] private float m_fadeSpeed;
    [SerializeField] private Animator m_animator;

    [SerializeField] private bool m_areWallsOn = true;

    private void Update()
    {
        //Debug.Log("alpha: " + m_thingsToRemove[0].materials[1].color.a);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("kitchenWalls"))
        {
            Debug.Log("kitchenWalls");
            if (m_areWallsOn)
            {
                m_animator.SetTrigger("goDown");
                //for (int i = 0; i < m_thingsToRemove.Length; i++)
                //{
                //    StartCoroutine(FadeOut(i));
                //}
            }
            else
            {
                m_animator.SetTrigger("goUp");
                //for (int i = 0; i < m_thingsToRemove.Length; i++)
                //{
                //    StartCoroutine(FadeIn(i));
                //}
            }
            m_areWallsOn = !m_areWallsOn;
        }
    }

    //private IEnumerator FadeOut(int i)
    //{
    //    Debug.Log("fade out");
    //    MeshRenderer meshRenderer = m_thingsToRemove[i].GetComponent<MeshRenderer>();
    //    Color color = m_thingsToRemove[i].materials[1].color;
    //
    //    while (color.a > 0)
    //    {
    //        color.a -= 1;
    //
    //        meshRenderer.materials[1].color = color;
    //        Debug.Log("alpha: " + meshRenderer.materials[1].color.a);
    //        yield return new WaitForEndOfFrame();
    //        //yield return new WaitForSeconds(m_fadeSpeed);
    //    }
    //    yield return new WaitUntil(() => meshRenderer.materials[0].color.a <= 0f);
    //}
    //
    //private IEnumerator FadeIn(int i)
    //{
    //    Debug.Log("fade in");
    //    MeshRenderer meshRenderer = m_thingsToRemove[i].GetComponent<MeshRenderer>();
    //    Color color = m_thingsToRemove[i].materials[1].color;
    //    while (color.a < 255)
    //    {
    //        color.a += 1;
    //
    //        meshRenderer.materials[1].color = color;
    //        Debug.Log("alpha: " + meshRenderer.materials[1].color.a);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return new WaitUntil(() => meshRenderer.materials[0].color.a <= 0f);
    //}
}