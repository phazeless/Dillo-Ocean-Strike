using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enviorment_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public SpriteRenderer m_SpriteRenderer;
    public Animator m_Anim;
    public float m_Speed;
    public Sprite m_NormalSprite;
    public Sprite m_FeverSprite;
    //===== PRIVATES =====
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================

    void Start(){
        
    }

    void Update(){
        if (m_Anim != null) {
            if(m_Speed!=0) m_Anim.SetFloat("Speed", m_Speed);
        }
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init() {
        if (GameManager_Manager.m_Instance.f_IsGameFever()) {
            if(m_SpriteRenderer!=null) m_SpriteRenderer.sprite = m_FeverSprite;
            if(m_Anim!=null) m_Anim.SetBool("Storm", true);
        }
        else {
            if (m_SpriteRenderer != null)  m_SpriteRenderer.sprite = m_NormalSprite;
            if (m_Anim != null) m_Anim.SetBool("Storm", false);
        }
    }

    public void f_ChangeToFever() {
        if (m_SpriteRenderer != null) m_SpriteRenderer.sprite = m_FeverSprite;
        if (m_Anim != null) m_Anim.SetBool("Storm", true);
    }

    public virtual void f_RevertToNormal() {
        if (m_SpriteRenderer != null) m_SpriteRenderer.sprite = m_NormalSprite;
        if (m_Anim != null) m_Anim.SetBool("Storm", false);
    }
}
