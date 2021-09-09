using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Thunder_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Thunder_Gameobject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public Animator m_Anim;
    public float m_Speed =1.0f;
    public AudioClip m_ThunderSound;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Start(){
        m_Anim.SetFloat("Speed", m_Speed);
    }

    void Update(){
        
    }
    private void OnEnable() {
        //Audio_Manager.m_Instance.f_PlayOneShot(m_ThunderSound);
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init(int p_Index) {
        m_Anim.SetInteger("Index",p_Index);
    }

    public void f_Disable() {
        gameObject.SetActive(false);
    }
}
