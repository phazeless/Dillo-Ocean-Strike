using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
