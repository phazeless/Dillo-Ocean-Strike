using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cloud_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public SpriteRenderer m_SpriteRenderer;
    public Animator m_Anim;
    public Sprite m_NormalSprite;
    public Sprite m_FeverSprite;
    public float m_Speed;
    public int m_TypeID;
    //===== PRIVATES =====
    Vector3 m_MoveToPos;
    Vector3 t_Vector;
    public Vector2 m_TargetPos;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Start(){
        
    }

    void Update(){
        if (Vector2.Distance(transform.position, m_TargetPos) > .1f) {
            m_MoveToPos = Vector2.MoveTowards(transform.position, m_TargetPos, m_Speed * Time.deltaTime);
            transform.position = m_MoveToPos;
        }
        else {
            Seagull_Manager.m_Instance.m_CurrentBirdCount--;
            gameObject.SetActive(false);
        }
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_ChangeToFever() {
         m_SpriteRenderer.sprite = m_FeverSprite;
    }

    public virtual void f_RevertToNormal() {
        m_SpriteRenderer.sprite = m_NormalSprite;
    }

    public void f_Init(Vector3 p_TargetPos,float p_Speed,float p_Scale) {
        if (GameManager_Manager.m_Instance.f_IsGameFever()) {
             m_SpriteRenderer.sprite = m_FeverSprite;
        }
        else {
             m_SpriteRenderer.sprite = m_NormalSprite;
        }

        m_Speed = p_Speed;
        
        t_Vector.x = p_Scale;
        t_Vector.y = p_Scale;
        t_Vector.z = 1f;

        transform.localScale = t_Vector;
        m_TargetPos = p_TargetPos;
    }
}
