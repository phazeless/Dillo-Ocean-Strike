using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;

public class Player_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Player_Gameobject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public e_PlayerMovement m_PlayerState;
    [Header("Status")]
    public float m_Radius;
    public float m_Speed;
    public float m_DefaultSpeed;
    public int m_TapInterval;
    public float m_IncreaseSpeed = .1f;
    public GameObject m_FeverEffect;
    [Header("Target")]
    public Transform m_LeftLocation;
    public Transform m_RightLocation;
    public Transform m_Default;
    //===== PRIVATES =====
    Vector3 m_DefaultPos;
    Vector3 m_MoveToPos;
    Vector2 m_TargetPos;
    int t_TapCount =0;
    float t_Time;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        gameObject.SetActive(false);
    }

    void Update(){
        if (GameManager_Manager.m_Instance.f_IsGamePlaying() && m_PlayerState!=e_PlayerMovement.Stop) {
            if (Vector2.Distance(transform.position, m_TargetPos) > .1f) {
                m_MoveToPos = Vector2.MoveTowards(transform.position, m_TargetPos, m_Speed * Time.deltaTime);
                m_MoveToPos.z = 100f;
                transform.position = m_MoveToPos;
            }
            else {
                f_SetTarget();
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init() {
        t_TapCount = 0;
        m_PlayerState = e_PlayerMovement.Left;
        m_Speed = m_DefaultSpeed;
        f_SetTarget();
        transform.position = m_Default.position;
    }
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

    public void f_Tap() {
        t_TapCount++;
        if (t_TapCount >= m_TapInterval) {
            t_TapCount = 0;
            f_IncreaseSpeed();
        }
    }

    public void f_FeverMode() {
        transform.position = Goal_Gameobject.m_Instance.transform.position;
        m_PlayerState = e_PlayerMovement.Stop;
        m_FeverEffect.SetActive(true);
    }

    public void f_BackToNormal() {
        m_FeverEffect.SetActive(false);
        f_SetTarget();
    }

    public void f_IncreaseSpeed() {
        m_Speed += m_IncreaseSpeed;
    }
}
