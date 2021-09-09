using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wardobe_Manager : MonoBehaviour {
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Wardobe_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public Animator m_Popup;
    public int m_BoatEquipedID = 0;
    public int m_RodEquipedID = 0;
    public Animator m_BoatAnimator;
    public SpriteRenderer m_BoatSpriteRender;
    public Animator m_RodAnimator;
    public SpriteRenderer m_RodSpriteRenderer;
    public List<BoatSkin_Scriptable> m_BoatData;
    public List<CharacterSkin_Scriptable> m_RodData;
    public GameObject m_BoatPrice;
    public TextMeshProUGUI m_BoatPriceText;
    public GameObject m_RodPrice;
    public TextMeshProUGUI m_RodPriceText;
    public GameObject m_PurchaseMenu;
    public TextMeshProUGUI m_CurrentMoney;
    public TextMeshProUGUI m_CalculatedMoney;
    [Header("Mats")]
    public Material m_UnlockedMaterial;
    public Material m_LockedMaterial;
    public GameObject m_Bling;
    [Header("Audio")]
    public AudioClip m_Buy;
    public AudioClip m_Get;
    //===== PRIVATES =====
    int m_BoatCurrentIndex = 0;
    int m_RodCurrentIndex = 0;
    int t_BoatPrices = 0;
    int t_RodPrices = 0;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init() {
        m_BoatCurrentIndex = f_GetCurrenBoatIndex();
        m_RodCurrentIndex = f_GetCurrentRodIndex();
        t_BoatPrices = 0;
        t_RodPrices = 0;
    }

    public int f_GetCurrenBoatIndex() {
        for (int i = 0; i < m_BoatData.Count; i++) {
            if (m_BoatEquipedID == m_BoatData[i].m_ID) {
                return i;
            }
        }
        return 0;
    }

    public int f_GetCurrentRodIndex() {
        for (int i = 0; i < m_RodData.Count; i++) {
            if (m_RodEquipedID == m_RodData[i].m_ID) {
                return i;
            }
        }
        return 0;
    }

    public void f_NextBoat() {
        m_BoatCurrentIndex++;
        if (m_BoatCurrentIndex > m_BoatData.Count - 1) m_BoatCurrentIndex = m_BoatData.Count - 1;
        m_BoatAnimator.runtimeAnimatorController = m_BoatData[m_BoatCurrentIndex].m_BoatAnimator;
        if (m_BoatData[m_BoatCurrentIndex].m_Locked) {
            m_BoatSpriteRender.material = m_LockedMaterial;
            m_BoatPrice.SetActive(true);
            m_BoatPriceText.text = m_BoatData[m_BoatCurrentIndex].m_Price.ToString();
        }
        else {
            m_BoatSpriteRender.material = m_UnlockedMaterial;
            m_BoatPrice.SetActive(false);
        }
    }

    public void f_PreviousBoat() {
        m_BoatCurrentIndex--;
        if (m_BoatCurrentIndex < 0) m_BoatCurrentIndex = 0;
        m_BoatAnimator.runtimeAnimatorController = m_BoatData[m_BoatCurrentIndex].m_BoatAnimator;
        if (m_BoatData[m_BoatCurrentIndex].m_Locked) {
            m_BoatSpriteRender.material = m_LockedMaterial;
            m_BoatPrice.SetActive(true);
            m_BoatPriceText.text = m_BoatData[m_BoatCurrentIndex].m_Price.ToString();
        }
        else {
            m_BoatSpriteRender.material = m_UnlockedMaterial;
            m_BoatPrice.SetActive(false);
        }
    }

    public void f_NextRod() {
        m_RodCurrentIndex++;

        if (m_RodCurrentIndex > m_RodData.Count - 1) m_RodCurrentIndex = m_RodData.Count - 1;

        m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;

        if (m_RodData[m_RodCurrentIndex].m_Locked) {
            m_RodSpriteRenderer.material = m_LockedMaterial;
            m_RodPrice.gameObject.SetActive(true);
            m_RodPriceText.text = m_RodData[m_RodCurrentIndex].m_Price.ToString();
        }
        else {
            m_RodSpriteRenderer.material = m_UnlockedMaterial;
            m_RodPrice.SetActive(false);
        }
        if (m_RodData[m_RodCurrentIndex].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
    }

    public void f_PreviousRod() {
        m_RodCurrentIndex--;
        if (m_RodCurrentIndex < 0) m_RodCurrentIndex = 0;
        m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;
        if (m_RodData[m_RodCurrentIndex].m_Locked) {
            m_RodSpriteRenderer.material = m_LockedMaterial;
            m_RodPrice.gameObject.SetActive(true);
            m_RodPriceText.text = m_RodData[m_RodCurrentIndex].m_Price.ToString();
           
        }
        else {
            m_RodSpriteRenderer.material = m_UnlockedMaterial;
            m_RodPrice.SetActive(false);
        }
        if (m_RodData[m_RodCurrentIndex].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
    }

    public void f_Back() {
        m_BoatPrice.SetActive(false);
        m_RodPrice.SetActive(false);

        m_BoatAnimator.runtimeAnimatorController = m_BoatData[f_GetCurrenBoatIndex()].m_BoatAnimator;
        m_BoatSpriteRender.material = m_UnlockedMaterial;

        m_RodAnimator.runtimeAnimatorController = m_RodData[f_GetCurrentRodIndex()].m_ObjectAnimator;
        m_RodSpriteRenderer.material = m_UnlockedMaterial;
        if (m_RodData[f_GetCurrentRodIndex()].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
    }

    public void f_Confirm() {
        if (m_BoatData[m_BoatCurrentIndex].m_Locked) {
            t_BoatPrices = m_BoatData[m_BoatCurrentIndex].m_Price;
        }
        else {
            m_BoatEquipedID = m_BoatData[m_BoatCurrentIndex].m_ID;
            Boat_Gameobject.m_Instance.m_ScoreMultiplier = m_BoatData[m_BoatCurrentIndex].m_ScoreMultiplier;
            t_BoatPrices = 0;
        }

        if (m_RodData[m_RodCurrentIndex].m_Locked) {
            t_RodPrices = m_RodData[m_RodCurrentIndex].m_Price;
        }
        else {
            m_RodEquipedID = m_RodData[m_RodCurrentIndex].m_ID;
            t_RodPrices = 0;
            if (m_RodData[f_GetCurrentRodIndex()].m_ID == 7) m_Bling.SetActive(true);
            else m_Bling.SetActive(false);
        }

        if (t_BoatPrices + t_RodPrices > 0) {
            m_PurchaseMenu.SetActive(true);
            m_CurrentMoney.text = Player_Manager.m_Instance.m_Currency.ToString("0");
            m_CalculatedMoney.text = (Player_Manager.m_Instance.m_Currency - (t_BoatPrices + t_RodPrices)).ToString("0");
            if (Player_Manager.m_Instance.m_Currency - (t_BoatPrices + t_RodPrices) < 0) {
                m_CalculatedMoney.color = Color.red;
            }
            else {
                m_CalculatedMoney.color = Color.white;
            }
        }
        else {
            m_Popup.Play("close");
        }
    }

    public void f_Buy() {
        if (Player_Manager.m_Instance.m_Currency >= (t_BoatPrices + t_RodPrices)) {
            PurchaseMenu_Gameobject.m_Instance.f_Deactivate();
            Player_Manager.m_Instance.m_Currency -= (t_BoatPrices + t_RodPrices);

            m_BoatAnimator.runtimeAnimatorController = m_BoatData[m_BoatCurrentIndex].m_BoatAnimator;
            m_BoatData[m_BoatCurrentIndex].m_Locked = false;
            m_BoatSpriteRender.material = m_UnlockedMaterial;

            m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;
            m_RodData[m_RodCurrentIndex].m_Locked = false;
            m_RodSpriteRenderer.material = m_UnlockedMaterial;
            m_BoatPrice.SetActive(false);
            m_RodPrice.SetActive(false);
            Audio_Manager.m_Instance.f_PlayOneShot(m_Buy);
            Audio_Manager.m_Instance.f_PlayOneShot(m_Get);
        }
    }


}
