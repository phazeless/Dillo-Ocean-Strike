using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public List<GameObject> m_Go;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Start(){
        
    }

    void Update(){
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init(int p_ComboIndex) {
        for (int i = 0; i < m_Go.Count; i++) {
            if (p_ComboIndex == i) {
                m_Go[i].SetActive(true);
            }
            else m_Go[i].SetActive(false);
        }
    }

    public void f_Disable() {
        gameObject.SetActive(false);
    }
}
