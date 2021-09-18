using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardPool_Manager : ObjectPooler.PoolingManager_Manager<LeaderboardData_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static LeaderboardPool_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public LeaderboardData_Gameobject m_Player;
    //===== PRIVATES =====
    Vector3 t_Vector;
    LeaderboardData_Gameobject t_Temp;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        
    }

    void Update(){
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Spawn(string p_Place,string p_Name,string p_Value) {
        t_Temp = f_SpawnObject();
        t_Vector = t_Temp.transform.position;
        t_Vector.z = 100;
        t_Temp.transform.position = t_Vector;
        t_Temp.f_Init((int.Parse(p_Place)+1).ToString(),p_Name,p_Value);
        t_Temp.gameObject.SetActive(true);
    }

    public void f_InitPlayer(string p_Place, string p_Name, string p_Value) {
        m_Player.f_Init((int.Parse(p_Place) + 1).ToString(), p_Name, p_Value);
    }

    public void f_DeactivateAll() {
        for (int i = 0; i < m_PoolingContainer.Count; i++) {
            m_PoolingContainer[i].gameObject.SetActive(false);
        }
    }
}
