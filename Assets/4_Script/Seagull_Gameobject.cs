using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Seagull_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====
    //===== PUBLIC =====
    public float m_Speed;
    //===== PRIVATES =====
    Vector3 m_MoveToPos;
    Vector2 m_TargetPos;
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
    public void f_Init(Vector3 p_TargetPos,float p_Speed) {
        m_Speed = p_Speed;
        m_TargetPos = p_TargetPos;
    }
}
