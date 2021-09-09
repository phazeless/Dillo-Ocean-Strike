using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TapFxTest_Manager : ObjectPooler.PoolingManager_Manager<TapEffect_Gameobject>{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static TapFxTest_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public int m_Index = 0;
    //===== PRIVATES =====
    int t_Index;
    TapEffect_Gameobject t_Temp;
    Vector3 t_Vector;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }

    public void Update() {

    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_SpawnTap(int p_Index) {
        t_Index = p_Index;
        t_Vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        t_Vector.z = Camera.main.nearClipPlane;
        t_Temp = f_SpawnObject();
        t_Temp.transform.position = t_Vector;
        t_Temp.f_Init();
        t_Temp.gameObject.SetActive(true);
    }

    public override bool f_AdditionalValidation(int p_Index) {
        return m_PoolingContainer[p_Index].m_TypeID == t_Index;
    }

    public override int f_GetType() {
        return t_Index;
    }
}
