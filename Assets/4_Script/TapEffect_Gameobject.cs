using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;
public class TapEffect_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public int m_TypeID = 0;
    public float m_DeadTimer=3f;
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
    public void f_Init() {
        Timing.RunCoroutine(ie_WaitForDead());
    }

    IEnumerator<float> ie_WaitForDead() {
        yield return Timing.WaitForSeconds(m_DeadTimer);
        gameObject.SetActive(false);
    }
}
