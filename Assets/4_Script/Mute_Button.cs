using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;
public class Mute_Button : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Mute_Button m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public e_Mute m_MuteState;
    public AudioListener m_AudioListener;
    public Image m_Button;
    public Sprite m_MuteSprite;
    public Sprite m_NotMuteSprite;
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
    public void f_Mute() {
        if (m_MuteState == e_Mute.NotMute) {
            AudioListener.volume = 0;
            m_MuteState = e_Mute.Mute;
            m_Button.sprite = m_NotMuteSprite;
        }
        else {
            AudioListener.volume = 1;
            m_MuteState = e_Mute.NotMute;
            m_Button.sprite = m_MuteSprite;
        }
    }
}
