using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ObjectPooler;
using MEC;

public class Seagull_Manager : PoolingManager_Manager<Seagull_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Seagull_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public int m_MaxBird;
    public int m_CurrentBirdCount;
    public float m_MinSpeed = .5f;
    public float m_MaxSpeed = .75f;
    public Transform m_StartingArea;
    public Transform m_EndingArea;
    //===== PRIVATES =====
    Vector3 t_Vector;
    Seagull_Gameobject t_Bird;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        Timing.RunCoroutine(ie_Spawn());
    }

    void Update(){
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================

    IEnumerator<float> ie_Spawn() {
        do {
            if (m_CurrentBirdCount < m_MaxBird && !GameManager_Manager.m_Instance.f_IsGameFever()) {
                f_SpawnSeagull();
            }
            yield return Timing.WaitForSeconds(3f);
        } while (true);
    }

    public void f_SpawnSeagull() {
        t_Bird = f_SpawnObject();
        t_Vector = m_StartingArea.position;
        t_Vector.y = Random.Range(m_StartingArea.position.y, m_EndingArea.position.y);
        t_Bird.transform.position = t_Vector;
        t_Vector.x = m_EndingArea.position.x;
        t_Bird.f_Init(t_Vector, Random.Range(m_MinSpeed, m_MaxSpeed));
        m_CurrentBirdCount++;
        t_Bird.gameObject.SetActive(true);
    }

    public void f_DisableAll() {
        for (int i = 0; i < m_PoolingContainer.Count; i++) {
            m_PoolingContainer[i].gameObject.SetActive(false);
        }
    }

}
