using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardData_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public TextMeshProUGUI m_Place;
    public TextMeshProUGUI m_Names;
    public TextMeshProUGUI m_Score;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Start(){
        
    }

    void Update(){
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init(string p_Place, string p_Name, string p_Score) {
        m_Place.text = p_Place;
        m_Names.text = p_Name;
        m_Score.text = p_Score;
    }
}
