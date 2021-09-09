using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointText_Manager : ObjectPooler.PoolingManager_Manager<TextPopUp_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static PointText_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====

    //===== PRIVATES =====
    TextPopUp_Gameobject t_Temp;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SpawnPoint(int p_Score,Vector3 p_Pos) {
        //t_Temp.transform.localScale = new Vector3(1, 1, 1);
        t_Temp = f_SpawnObject();
        t_Temp.transform.position = p_Pos;
        t_Temp.f_Initialize(p_Score);
        t_Temp.gameObject.SetActive(true);
    }
}
