using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboText_Manager : ObjectPooler.PoolingManager_Manager<ComboText_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static ComboText_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public Transform m_SpawnPosTopLeft;
    public Transform m_SpawnPosTopRight;
    //===== PRIVATES =====
    Vector3 t_Pos;
    ComboText_Gameobject t_Temp;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SpawnPoint(int p_Score) {
        t_Temp = f_SpawnObject();
        t_Pos = m_SpawnPosTopLeft.position;
        t_Pos.x = Random.Range(m_SpawnPosTopLeft.position.x, m_SpawnPosTopRight.position.x);
        t_Pos.y = Random.Range(m_SpawnPosTopLeft.position.y, m_SpawnPosTopRight.position.y);
        t_Temp.transform.position = t_Pos;
        t_Temp.f_Init(p_Score);
        t_Temp.gameObject.SetActive(true);
    }
}
