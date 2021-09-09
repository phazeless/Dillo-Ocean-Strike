using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;
public class MovingUI_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====
    //===== PUBLIC =====
    public e_PlayerMovement m_PlayerState;
    public float m_Speed;
    public float m_DefaultSpeed;
    public float m_StormSpeed;
    [Header("Target")]
    public Transform m_LeftLocation;
    public Transform m_RightLocation;
    //===== PRIVATES =====
    Vector3 m_DefaultPos;
    Vector3 m_MoveToPos;
    Vector2 m_TargetPos;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Start() {
        m_PlayerState = e_PlayerMovement.Left;
        m_DefaultPos = transform.position;
        m_DefaultPos.z = 100f;
        m_Speed = m_DefaultSpeed;
        if (m_StormSpeed < 0) m_StormSpeed = m_DefaultSpeed;
        f_SetTarget();
    }

    void Update() {
        if (Vector2.Distance(transform.position, m_TargetPos) > .1f) {
            m_MoveToPos = Vector2.MoveTowards(transform.position, m_TargetPos, m_Speed * Time.deltaTime);
            m_MoveToPos.z = 100f;
            transform.position = m_MoveToPos;
        }
        else {
            f_SetTarget();
        }
    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SetTarget() {
        if (m_PlayerState == e_PlayerMovement.Left) {
            m_PlayerState = e_PlayerMovement.Right;
            m_TargetPos = m_RightLocation.transform.position;
        }
        else {
            m_PlayerState = e_PlayerMovement.Left;
            m_TargetPos = m_LeftLocation.transform.position;
        }
    }
}
