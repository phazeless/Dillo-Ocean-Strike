using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Player_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public double m_Currency = 10000;
    public int m_MultiplierLevel = 1;
    public double m_MultiplierProgress = 0;
    public double m_MaxMultiplierProgress = 1000;
    public Image m_Bar;
    public TextMeshProUGUI m_LevelMultiplierText;
    public TextMeshProUGUI m_inLevelMultiplierText;
    public TextMeshProUGUI m_MaxMultiplierText;
    public TextMeshProUGUI m_ProgressText;
    //===== PRIVATES =====
    float t_MultiplerProgress = 0;
    float t_MaxMultiplerProgress = 1000;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        
    }

    void Update(){
        m_inLevelMultiplierText.text = m_MultiplierLevel.ToString("000");
        m_LevelMultiplierText.text = m_MultiplierLevel.ToString("00");
        m_ProgressText.text = m_MultiplierProgress.ToString("0000");
        m_MaxMultiplierText.text = m_MaxMultiplierProgress.ToString();
        m_Bar.fillAmount = (float)(m_MultiplierProgress / m_MaxMultiplierProgress);
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
}
