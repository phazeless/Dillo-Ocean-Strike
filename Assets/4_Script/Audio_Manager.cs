using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;

public class Audio_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Audio_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public AudioSource m_AmbienceSource;
    public AudioSource m_BGMSource;
    public AudioSource m_OneShotSource;
    public e_Mute m_MuteState;
    public Image[] m_Button;
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
    public void f_ChangeAmbience(AudioClip p_AudioClip) {
        m_AmbienceSource.clip = p_AudioClip;
        m_AmbienceSource.Play();
    }
    public void f_ChangeBGM(AudioClip p_AudioClip) {
        m_BGMSource.clip = p_AudioClip;
        m_BGMSource.Play();
    }

    public void f_PlayOneShot(AudioClip p_Audio) {
        m_OneShotSource.PlayOneShot(p_Audio);
    }

    public void f_CheckMute() {
        if (m_MuteState == e_Mute.NotMute) {
            AudioListener.volume = 0;
            m_MuteState = e_Mute.Mute;
            for (int i = 0; i < m_Button.Length; i++) {
                m_Button[i].sprite = m_NotMuteSprite;
            }
        }
        else {
            AudioListener.volume = 1;
            m_MuteState = e_Mute.NotMute;
            for (int i = 0; i < m_Button.Length; i++) {
                m_Button[i].sprite = m_MuteSprite;
            }
        }
    }
}
