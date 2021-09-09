using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseMenu_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static PurchaseMenu_Gameobject m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public Animator m_Anim;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Deactivate() {
        m_Anim.Play("Close");
    }
    public void f_Disable() {
        gameObject.SetActive(false);
    }
}
