using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static StartMenu_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public List<StartMenuID_Gameobject> m_StartMenus;
    //===== PRIVATES =====

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
    public void f_Deactivate(int p_ID) {
        for (int i = 0; i < m_StartMenus.Count; i++) {
            if (m_StartMenus[i].m_ID != p_ID) m_StartMenus[i].gameObject.SetActive(false);
        }
    }

    public void f_Activate() {
        for (int i = 0; i < m_StartMenus.Count; i++) {
            m_StartMenus[i].gameObject.SetActive(true);
        }
    }
}
