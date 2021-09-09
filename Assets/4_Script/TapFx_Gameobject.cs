using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;

public class TapFx_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public List<GameObject> m_Go;
    public float m_Duration;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
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
        Timing.RunCoroutine(ie_Destroy());
    }

    IEnumerator<float> ie_Destroy() {
        yield return Timing.WaitForSeconds(m_Duration);
        gameObject.SetActive(false);
    }

}
