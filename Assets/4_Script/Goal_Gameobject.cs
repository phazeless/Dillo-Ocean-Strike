using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;
public class Goal_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Goal_Gameobject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public e_GoalMovement m_GoalState;
    [Header("Status")]
    public float m_Radius;
    public float m_Speed;
    public float m_DefaultSpeed;
    public float m_StartTapInterval = 25;
    public float m_TapInterval = 10f;
    public float m_IncreaseSpeed;
    public float m_MaxSpeed = 1.5f;
    [Header("Target")]
    public Transform m_LeftLocation;
    public Transform m_RightLocation;
    public Transform m_Default;
    //===== PRIVATES =====
    Vector3 m_DefaultPos;
    Vector3 m_MoveToPos;
    Vector2 m_TargetPos;
    int t_TapCount;
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
        if (GameManager_Manager.m_Instance.f_IsGamePlaying()) {
            if (m_GoalState != e_GoalMovement.Stop) {
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
        m_GoalState = e_GoalMovement.Stop;
        m_Speed = m_DefaultSpeed;
        transform.position = m_Default.position;
    }
    public void f_OnTap() {
        t_TapCount++;
        if (m_GoalState == e_GoalMovement.Stop) {
            if (t_TapCount >= m_StartTapInterval) {
                t_TapCount = 0;
                m_GoalState = e_GoalMovement.Left;
                f_SetTarget();
            }
        }
        else {
            if (t_TapCount >= m_TapInterval) {
                t_TapCount = 0;
                f_IncreaseSpeed();
            }
        }
    }
    public void f_SetTarget() {
        if (m_GoalState == e_GoalMovement.Left) {
            m_GoalState = e_GoalMovement.Right;
            m_TargetPos = m_RightLocation.transform.position;
        }
        else {
            m_GoalState = e_GoalMovement.Left;
            m_TargetPos = m_LeftLocation.transform.position;
        }
    }

    public void f_IncreaseSpeed() {
        m_Speed += m_IncreaseSpeed;
        if (m_Speed > m_MaxSpeed) m_Speed = m_MaxSpeed;
    }
}
