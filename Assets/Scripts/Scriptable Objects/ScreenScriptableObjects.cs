using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenScriptableObjects", menuName = "ScreenInteractionScriptableObject")]
public class ScreenScriptableObjects : ScriptableObject
{
    public float m_amountToMoveLeftRight;
    public float m_amountToMoveTopBottom;
}
