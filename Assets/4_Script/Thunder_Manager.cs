using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;
public class Thunder_Manager : ObjectPooler.PoolingManager_Manager<Thunder_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Thunder_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public float m_Interval = .1f;
    public Transform m_LeftPoint;
    public Transform m_RightPoint;
    //===== PRIVATES =====
    Vector3 t_Position;
    Thunder_Gameobject t_Temp;
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
            if (GameManager_Manager.m_Instance.f_IsGameFever()) {
                t_Temp = f_SpawnObject();
                t_Position = m_LeftPoint.transform.position;
                t_Position.x = Random.Range(m_LeftPoint.transform.position.x, m_RightPoint.position.x);
                t_Temp.transform.localScale = m_Objects[0].transform.localScale;
                t_Temp.transform.position = t_Position;
                t_Temp.f_Init(Random.Range(1, 4));
                t_Temp.gameObject.SetActive(true);
            }
            yield return Timing.WaitForSeconds(m_Interval);
        } while (true);
    }
}
