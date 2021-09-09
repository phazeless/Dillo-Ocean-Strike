using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboText_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    //===== STRUCT =====

    //===== PUBLIC =====
    public TextMeshProUGUI m_Text;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init(int p_CurrCombo) {
        m_Text.text = p_CurrCombo.ToString();
    }

    public void f_Disable() {
        gameObject.SetActive(false);
    }
}
