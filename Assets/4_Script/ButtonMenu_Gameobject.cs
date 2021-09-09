using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class c_Event : UnityEvent { }

public class ButtonMenu_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====
    //===== PUBLIC =====
    public Transform m_TargetPos;
    public c_Event m_OnMoveToMiddleDone;
    public c_Event m_OnMoveBackDone;
    public Transform m_DefaultPos;
    public float m_DurationToTarget = 1.0f;
    public float m_DurationBack = 1.0f;
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
    public void f_MoveToTarget() {
        iTween.MoveTo(gameObject, iTween.Hash("position",m_TargetPos.position,"time", m_DurationToTarget, "oncomplete","f_OnMoveToMiddleDone"));
    }

    public void f_MoveBack() {
        iTween.MoveTo(gameObject, iTween.Hash("position", m_DefaultPos.position, "time", m_DurationBack, "oncomplete", "f_OnMoveBackDone"));
    }
    public void f_OnMoveToMiddleDone() {
        m_OnMoveToMiddleDone?.Invoke();    
    }
    public void f_OnMoveBackDone() {
        m_OnMoveBackDone?.Invoke();
    }
}
