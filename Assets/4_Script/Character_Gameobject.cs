using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character_Gameobject : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Character_Gameobject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public Animator m_Dillo;
    public Animator m_Rod;
    public Animator m_Fishes;
    public GameObject m_FishesParent;
    public List<GameObject> m_Fish;
    public int m_PullCount = 0;
    public Queue<int> m_FishQueue = new Queue<int>();
    public AudioClip m_Hit;
    public AudioClip m_Miss;
    //===== PRIVATES =====
    int t_Index;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        
    }

    void Update(){
        m_Dillo.SetInteger("PullCount", m_PullCount);
        m_Rod.SetInteger("PullCount", m_PullCount);
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Pull(bool p_Miss, int p_Fish = 5) {
        if (m_PullCount <= 0) {
            m_Dillo.SetTrigger("Pull");
            m_Rod.SetTrigger("Pull");
        }
        else {
            m_Dillo.SetFloat("PullSpeed", 1.5f);
            m_Rod.SetFloat("PullSpeed", 1.5f);
            m_Fishes.SetFloat("PullSpeed", 1.5f);
        }

        m_PullCount++;

        m_FishQueue.Enqueue(p_Fish);

        if (!p_Miss) {
            m_FishesParent.gameObject.SetActive(true);
        }
        else {
            m_FishesParent.gameObject.SetActive(false);
        }

        m_Dillo.SetBool("Miss", p_Miss);
        m_Rod.SetBool("Miss", p_Miss);
    }

    public void f_DecreasePullCount() {
        m_Dillo.SetFloat("PullSpeed", 1.0f);
        m_Rod.SetFloat("PullSpeed", 1.0f);
        m_Fishes.SetFloat("PullSpeed", 1.0f);
        m_Fishes.gameObject.SetActive(false);
    }

    public void f_GetFish() {
        m_Fishes.gameObject.SetActive(true);
        for (int i = 0; i < m_Fish.Count; i++) {
            m_Fish[i].gameObject.SetActive(false);
        }
        t_Index = m_FishQueue.Dequeue();

        if (t_Index != 5) {
            Audio_Manager.m_Instance.f_PlayOneShot(m_Hit);
            m_Fish[t_Index].gameObject.SetActive(true);
        }
        else {
            Audio_Manager.m_Instance.f_PlayOneShot(m_Miss);
        }
    }

    public void f_EarlyMinusPullCount() {
        m_PullCount--;
    }

    public void f_Reset() {
        m_PullCount = 0;
        m_Dillo.Play("Idle");
        m_Rod.Play("Idle");
        m_FishQueue.Clear();
        m_FishesParent.gameObject.SetActive(true);
        m_Fishes.gameObject.SetActive(false);
    }
}
