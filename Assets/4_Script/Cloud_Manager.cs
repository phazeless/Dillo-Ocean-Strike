using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ObjectPooler;
using MEC;
public class Cloud_Manager : PoolingManager_Manager<Cloud_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Cloud_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public float m_MinSpeed = .5f;
    public float m_MaxSpeed = .75f;
    public float m_MinSpawnTimer = 2f;
    public float m_MaxSpawnTimer = 4f;
    public float m_MinScale = .5f;
    public float m_MaxScale = 1f;
    public Transform m_StartingArea;
    public Transform m_EndingArea;
    //===== PRIVATES =====
    int m_Type;
    Vector3 t_Vector;
    Cloud_Gameobject t_Cloud;
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
                m_Type = Random.Range(0, m_Objects.Count - 1);
                t_Cloud = f_SpawnObject();
                t_Vector = m_StartingArea.position;
                t_Vector.y = Random.Range(m_StartingArea.position.y, m_EndingArea.position.y);
                t_Cloud.transform.position = t_Vector;
                t_Vector.x = m_EndingArea.position.x;
                t_Cloud.f_Init(t_Vector,Random.Range(m_MinSpeed,m_MaxSpeed),Random.Range(m_MinScale,m_MaxScale));
                t_Cloud.gameObject.SetActive(true);
                yield return Timing.WaitForSeconds(Random.Range(m_MinSpawnTimer,m_MaxSpawnTimer));
        } while (true);
    }

    public override bool f_AdditionalValidation(int p_Index) {
        return m_PoolingContainer[p_Index].m_TypeID == m_Objects[m_Type].m_TypeID;
    }

    public override int f_GetType() {
        return m_Type;
    }

    public void f_ChangeToStorm() {
        for (int i = 0; i < m_PoolingContainer.Count; i++) {
            m_PoolingContainer[i].f_ChangeToFever();
        }
    }


    public void f_RevertToNormal() {
        for (int i = 0; i < m_PoolingContainer.Count; i++) {
            m_PoolingContainer[i].f_RevertToNormal();
        }
    }
}
