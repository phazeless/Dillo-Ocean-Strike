using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Start_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public AudioClip m_Countdonw;
    public AudioClip m_Start;
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
    public void f_Start() {
        GameManager_Manager.m_Instance.f_Start();
        gameObject.SetActive(false);
    }

    public void f_PlayCount() {
        Audio_Manager.m_Instance.f_PlayOneShot(m_Countdonw);
    }
    public void f_PlayStart() {
        Audio_Manager.m_Instance.f_PlayOneShot(m_Start);
    }
}
