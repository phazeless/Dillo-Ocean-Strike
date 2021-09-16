using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
public class Player_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Player_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public float m_Currency = 10000;
    public int m_MultiplierLevel = 1;
    public float m_MultiplierProgress = 0;
    public float m_MaxMultiplierProgress = 1000;
    public int m_Place = 1;
    public string m_DisplaNames ="";
    public int m_Highscore = 0;
    public Image m_Bar;
    public TextMeshProUGUI m_inLevelMultiplierText;
    public TextMeshProUGUI m_MaxMultiplierText;
    public TextMeshProUGUI m_ProgressText;
    public TextMeshProUGUI m_CurrencyText;
    public TextMeshProUGUI m_PlaceText;
    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_ScoreText;
    public GameObject m_Loading;
    //===== PRIVATES =====
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
        m_ProgressText.text = m_MultiplierProgress.ToString("0000");
        m_MaxMultiplierText.text = m_MaxMultiplierProgress.ToString();
        m_Bar.fillAmount = (m_MultiplierProgress / m_MaxMultiplierProgress);
        m_CurrencyText.text = m_Currency.ToString("0");
        m_PlaceText.text = m_Place.ToString() +(m_Place == 1?"st":(m_Place==2?"nd":(m_Place==3?"rd":"th")));
        m_NameText.text = m_DisplaNames;
        m_ScoreText.text = m_Highscore.ToString();
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================

    public void f_UpdateMultiplierData() {
        PlayerStatistic_Manager.m_Instance.f_UpdateStatistics("MultiplierLevel", m_MultiplierLevel);
        PlayerStatistic_Manager.m_Instance.f_UpdateStatistics("MultiplierProgress", (int)m_MultiplierProgress);
    }

    public void f_LoadingStart() {
        m_Loading.gameObject.SetActive(true);
    }

    public void f_LoadingFinish() {
        m_Loading.gameObject.SetActive(false);
    }

    public void f_CheckInternetStart() {
        //Timing.RunCoroutine(ie_CheckInternet());
    }

    public void f_SetLeadeboard(int p_Place,string p_Name,int p_Score) {
        m_Place = p_Place;
        m_DisplaNames = p_Name;
        m_Highscore = p_Score;
    }
}
