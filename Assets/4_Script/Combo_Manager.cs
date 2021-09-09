using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo_Manager : ObjectPooler.PoolingManager_Manager<Combo_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Combo_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public Transform m_SpawnPos;
    //===== PRIVATES =====
    Combo_Gameobject t_Temp;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SpawnPoint(int p_Index) {
        t_Temp = f_SpawnObject();
        t_Temp.transform.position = m_SpawnPos.position;
        t_Temp.f_Init(p_Index);
        t_Temp.gameObject.SetActive(true);
    }
}
