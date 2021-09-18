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
    //===== STRUCT =====
    //===== PUBLIC =====
    [Header("Data")]
    public RuntimeAnimatorController m_ObjectAnimator;
    public int m_ID;
    public Sprite m_EffectImage;
    public bool m_Locked = true;
    public int m_Price = 100;
    [Header("Effect")]
    public float m_SmallFishMultiplier = 1.0f;
    public float m_MediumFishMultiplier = 1.0f;
    public float m_BigFishMultiplier = 1.0f;
    public float m_BerryChance = 0.0f;
    public bool m_SmallToMedium = false;
    public bool m_FeverMultiplier = false;
}
