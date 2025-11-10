using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenScriptableObjects", menuName = "ScreenInteractionScriptableObject")]
public class ScreenScriptableObjects : ScriptableObject
{
    public float m_amountToMoveX;
    public float m_amountToMoveY;
    public float m_amountToMoveW;
    public float m_amountToMoveH;

    public bool m_moveFromBottomToTop;
    public bool m_moveFromTopToBottom;
    public bool m_moveFromLeftToRight;
    public bool m_moveFromRightToLeft;

    public float m_finalFOVValue;
    public float m_finalViewportValue;
}
