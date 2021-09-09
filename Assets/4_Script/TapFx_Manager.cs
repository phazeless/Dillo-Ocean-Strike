using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TapFx_Manager : ObjectPooler.PoolingManager_Manager<TapFx_Gameobject> {
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static TapFx_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    //===== PRIVATES =====
    TapFx_Gameobject t_Temp;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SpawnPoint(int p_Index) {
        t_Temp = f_SpawnObject();
        t_Temp.transform.position = Goal_Gameobject.m_Instance.transform.position;
        t_Temp.f_Init(p_Index);
        t_Temp.gameObject.SetActive(true);
    }
}
