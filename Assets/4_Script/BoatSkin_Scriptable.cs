using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[CreateAssetMenu(fileName = "BoatSkin", menuName = "ScriptableObjects/BoatSkin", order = 1)]
public class BoatSkin_Scriptable : ScriptableObject{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== STRUCT =====
    //===== PUBLIC =====
    [Header("Data")]
    public RuntimeAnimatorController m_BoatAnimator;
    public int m_ID;
    public Sprite m_EffectImage;
    public string m_Lore;
    public bool m_Locked = true;
    public int m_Price = 100;
    [Header("Effect")]
    public float m_PlayerSpeed = 0f;
    public float m_FeverTime = 0f;
    public float m_FeverCount = 0.0f;
    public float m_HP = 1;    
}
