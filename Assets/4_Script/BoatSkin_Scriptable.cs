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
    public  RuntimeAnimatorController m_BoatAnimator;
    public int m_ID;
    public float m_ScoreMultiplier = 1.0f;
    public bool m_Locked = true;
    public int m_Price = 100;
}
