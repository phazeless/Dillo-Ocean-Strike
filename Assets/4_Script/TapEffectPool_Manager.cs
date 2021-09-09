using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ObjectPooler;
public class TapEffectPool_Manager : PoolingManager_Manager<TapEffect_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static TapEffectPool_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====

    //===== PRIVATES =====
    private int m_Type;
    TapEffect_Gameobject m_Temp;
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
    public void f_SpawnTap(int p_Type,Vector3 p_SpawnPos) {
        m_Type = p_Type;
        m_Temp = f_SpawnObject();
        m_Temp.transform.position = p_SpawnPos;
        m_Temp.f_Init();
        m_Temp.gameObject.SetActive(true);
    }

    public override bool f_AdditionalValidation(int p_Index) {
        return m_PoolingContainer[p_Index].m_TypeID == m_Objects[m_Type].m_TypeID;
    }

    public override int f_GetType() {
        return m_Type;
    }
}
