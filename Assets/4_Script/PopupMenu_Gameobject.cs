using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMenu_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====
    //===== PUBLIC =====
    public c_Event m_OnUnpopDone;
    public Vector3 m_TargetScale = new Vector3(1,1,1);
    public Vector3 m_BackScale = new Vector3(1, 0, 1);
    public float m_DurationPop = 1.0f;
    public float m_DurationUnpop = 1.0f;
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
    public void f_PopUp() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale",m_TargetScale,"time", m_DurationPop));
    }

    public void f_Unpop() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", m_BackScale, "time", m_DurationUnpop, "oncomplete", "f_OnUnpopupDone"));
    }

    public void f_OnUnpopupDone() {
        m_OnUnpopDone?.Invoke();
    }
}
