using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enviorment_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Enviorment_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public List<Enviorment_Gameobject> m_Enviorment;
    public GameObject m_OceanReflection;
    public GameObject m_Rain;
    public List<Thunder_Gameobject> m_Thunder;
    public Animator m_Flash;
    public AudioClip m_StormClip;
    public AudioClip m_SeaClip;
    public AudioClip m_FeverClip;
    public AudioClip m_NormalClip;
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
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_TransitionToStorm() {
        m_Flash.SetTrigger("Flash");
        for (int i = 0; i < m_Enviorment.Count; i++) {
            m_Enviorment[i].f_ChangeToFever();
        }

        for (int i = 0; i < m_Thunder.Count; i++) {
            m_Thunder[i].gameObject.SetActive(true);
        }

        Seagull_Manager.m_Instance.f_DisableAll();

        Cloud_Manager.m_Instance.f_ChangeToStorm();
        m_OceanReflection.SetActive(false);
        m_Rain.SetActive(true);
        Audio_Manager.m_Instance.f_ChangeAmbience(m_StormClip);
        Audio_Manager.m_Instance.f_ChangeBGM(m_FeverClip);

    }

    public void f_BackToNormal() {
        m_Flash.SetTrigger("Flash");
        for (int i = 0; i < m_Enviorment.Count; i++) {
            m_Enviorment[i].f_RevertToNormal();
        }

        for (int i = 0; i < m_Thunder.Count; i++) {
            m_Thunder[i].gameObject.SetActive(false);
        }

        Seagull_Manager.m_Instance.f_SpawnSeagull();

        Cloud_Manager.m_Instance.f_RevertToNormal();
        m_OceanReflection.SetActive(true);
        m_Rain.SetActive(false);
        Audio_Manager.m_Instance.f_ChangeAmbience(m_SeaClip);
        Audio_Manager.m_Instance.f_ChangeBGM(m_NormalClip);

    }
}
