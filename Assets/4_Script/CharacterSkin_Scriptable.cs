using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "CharacterSkin", menuName = "ScriptableObjects/CharacterSkin", order = 1)]
public class CharacterSkin_Scriptable : ScriptableObject{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static CharacterSkin_Scriptable m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public RuntimeAnimatorController m_ObjectAnimator;
    public int m_ID;
    public bool m_Locked = true;
    public int m_Price = 100;
}
