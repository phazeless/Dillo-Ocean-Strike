using System;
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
    public Button m_BuyRodButton;
    public Button m_BuyBoatButton;
    public Button m_ConfirmButton;
    public Image m_RodDesc;
    public Image m_BoatDesc;
    public TextMeshProUGUI m_Lore;
    [Header("Mats")]
    public Material m_UnlockedMaterial;
    public Material m_LockedMaterial;
    public GameObject m_Bling;
    [Header("Audio")]
    public AudioClip m_Buy;
    public AudioClip m_Get;
    //===== PRIVATES =====
    public Action m_OnDoneBuy;
    int m_BoatCurrentIndex = 0;
    int m_RodCurrentIndex = 0;
    int t_BoatPrices = 0;
    int t_RodPrices = 0;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
        gameObject.SetActive(false);
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_Init() {
        m_BoatCurrentIndex = f_GetCurrenBoatIndex();
        m_RodCurrentIndex = f_GetCurrentRodIndex();
        t_BoatPrices = 0;
        t_RodPrices = 0;
        f_ChangeDesc();
    }

    public void f_OnChangeRodOrBoat() {
        if (m_BoatData[m_BoatCurrentIndex].m_Locked || m_RodData[m_RodCurrentIndex].m_Locked) {
            m_ConfirmButton.interactable = false;
        }
        else {
            m_ConfirmButton.interactable = true;
        }

        if (t_BoatPrices> Player_Manager.m_Instance.m_Currency) {
            m_BuyBoatButton.interactable = false;
        }
        else {
            m_BuyBoatButton.interactable = true;
        }

        if (t_RodPrices > Player_Manager.m_Instance.m_Currency) {
            m_BuyRodButton.interactable = false;
        }else {
            m_BuyRodButton.interactable = true;
        }
        f_ChangeDesc();
    }

    public void f_ChangeDesc() {
        m_BoatDesc.sprite = m_BoatData[m_BoatCurrentIndex].m_EffectImage;
        m_RodDesc.sprite = m_RodData[m_RodCurrentIndex].m_EffectImage;
        if (!m_BoatData[m_BoatCurrentIndex].m_Locked) m_Lore.text = m_BoatData[m_BoatCurrentIndex].m_Lore;
        else m_Lore.text = "";
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
            t_BoatPrices = m_BoatData[m_BoatCurrentIndex].m_Price;
        }
        else {
            m_BoatSpriteRender.material = m_UnlockedMaterial;
            m_BoatPrice.SetActive(false);
            t_BoatPrices = 0;
        }

        f_OnChangeRodOrBoat();
    }

    public void f_PreviousBoat() {
        m_BoatCurrentIndex--;
        if (m_BoatCurrentIndex < 0) m_BoatCurrentIndex = 0;
        m_BoatAnimator.runtimeAnimatorController = m_BoatData[m_BoatCurrentIndex].m_BoatAnimator;
        if (m_BoatData[m_BoatCurrentIndex].m_Locked) {
            m_BoatSpriteRender.material = m_LockedMaterial;
            m_BoatPrice.SetActive(true);
            m_BoatPriceText.text = m_BoatData[m_BoatCurrentIndex].m_Price.ToString();
            t_BoatPrices = m_BoatData[m_BoatCurrentIndex].m_Price;
        }
        else {
            m_BoatSpriteRender.material = m_UnlockedMaterial;
            m_BoatPrice.SetActive(false);
            t_BoatPrices = 0;
        }
        f_OnChangeRodOrBoat();
    }

    public void f_NextRod() {
        m_RodCurrentIndex++;

        if (m_RodCurrentIndex > m_RodData.Count - 1) m_RodCurrentIndex = m_RodData.Count - 1;

        m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;

        if (m_RodData[m_RodCurrentIndex].m_Locked) {
            m_RodSpriteRenderer.material = m_LockedMaterial;
            m_RodPrice.gameObject.SetActive(true);
            m_RodPriceText.text = m_RodData[m_RodCurrentIndex].m_Price.ToString();
            t_RodPrices = m_RodData[m_RodCurrentIndex].m_Price;
        }
        else {
            m_RodSpriteRenderer.material = m_UnlockedMaterial;
            m_RodPrice.SetActive(false);
            t_RodPrices = 0;
        }
        if (m_RodData[m_RodCurrentIndex].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
        f_OnChangeRodOrBoat();
    }

    public void f_PreviousRod() {
        m_RodCurrentIndex--;
        if (m_RodCurrentIndex < 0) m_RodCurrentIndex = 0;
        m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;
        if (m_RodData[m_RodCurrentIndex].m_Locked) {
            m_RodSpriteRenderer.material = m_LockedMaterial;
            m_RodPrice.gameObject.SetActive(true);
            m_RodPriceText.text = m_RodData[m_RodCurrentIndex].m_Price.ToString();
            t_RodPrices = m_RodData[m_RodCurrentIndex].m_Price;
        }
        else {
            m_RodSpriteRenderer.material = m_UnlockedMaterial;
            m_RodPrice.SetActive(false);
            t_RodPrices = 0;
        }
        if (m_RodData[m_RodCurrentIndex].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
        f_OnChangeRodOrBoat();
    }

    public void f_Confirm() {
        if (!m_BoatData[m_BoatCurrentIndex].m_Locked && !m_RodData[m_BoatCurrentIndex].m_Locked) {
            m_BoatEquipedID = m_BoatData[m_BoatCurrentIndex].m_ID;
            m_RodEquipedID = m_RodData[m_RodCurrentIndex].m_ID;
            if (m_RodData[f_GetCurrentRodIndex()].m_ID == 7) m_Bling.SetActive(true);
            else m_Bling.SetActive(false);
            PlayerData_Manager.m_Instance.f_UpdatePlayerAvatarData(m_BoatEquipedID, m_RodEquipedID);
            m_Popup.Play("close");
        }
    }

    public void f_BuyBoat() {
        if (Player_Manager.m_Instance.m_Currency >= (t_BoatPrices)) {
            Player_Manager.m_Instance.f_LoadingStart();
            m_OnDoneBuy += f_onDoneBoughtBoat;
            CurrencyManager_Manager.m_Instance.f_RemoveVirtualCurrencyRequest((t_BoatPrices),f_Boughted);
        }
    }

    public void f_BuyRod() {
        if (Player_Manager.m_Instance.m_Currency >= (t_RodPrices)) {
            Player_Manager.m_Instance.f_LoadingStart();
            m_OnDoneBuy += f_onDoneBoughtRod;
            CurrencyManager_Manager.m_Instance.f_RemoveVirtualCurrencyRequest((t_RodPrices), f_Boughted);
        }
    }

    public void f_Boughted() {
        Audio_Manager.m_Instance.f_PlayOneShot(m_Buy);
        Audio_Manager.m_Instance.f_PlayOneShot(m_Get);
        f_FinishBought();
    }

    public void f_FinishBought() {
        m_OnDoneBuy?.Invoke();
        m_OnDoneBuy = null;
        Player_Manager.m_Instance.f_LoadingFinish();
    }

    public void f_onDoneBoughtRod() {
        m_RodAnimator.runtimeAnimatorController = m_RodData[m_RodCurrentIndex].m_ObjectAnimator;
        m_RodSpriteRenderer.material = m_UnlockedMaterial;
        m_RodData[m_RodCurrentIndex].m_Locked = false;
        f_OnChangeRodOrBoat();
        m_RodPrice.SetActive(false);
        f_UpdateWardobeData();
    }

    public void f_onDoneBoughtBoat() {
        m_BoatAnimator.runtimeAnimatorController = m_BoatData[m_BoatCurrentIndex].m_BoatAnimator;
        m_BoatSpriteRender.material = m_UnlockedMaterial;
        m_BoatData[m_BoatCurrentIndex].m_Locked = false;
        f_OnChangeRodOrBoat();
        m_BoatPrice.SetActive(false);
        f_UpdateWardobeData();
    }

    public void f_GetWardobeData(string p_BoatData,string p_RodData) {
        for (int i = 0; i < p_BoatData.Length; i++) {
            if (p_BoatData[i] == '0') m_BoatData[i].m_Locked = true;
            else m_BoatData[i].m_Locked = false;
        }

        for (int i = 0; i < p_RodData.Length; i++) {
            if (p_RodData[i] == '0') m_RodData[i].m_Locked = true;
            else m_RodData[i].m_Locked = false;
        }
    }

    public void f_SetClothes(string p_BoatClothes,string p_RodClothes) {
        m_BoatEquipedID = int.Parse(p_BoatClothes);
        m_RodEquipedID = int.Parse(p_RodClothes);

        m_BoatAnimator.runtimeAnimatorController = m_BoatData[f_GetCurrenBoatIndex()].m_BoatAnimator;
        m_BoatSpriteRender.material = m_UnlockedMaterial;

        m_RodAnimator.runtimeAnimatorController = m_RodData[f_GetCurrentRodIndex()].m_ObjectAnimator;
        m_RodSpriteRenderer.material = m_UnlockedMaterial;
        if (m_RodData[f_GetCurrentRodIndex()].m_ID == 7) m_Bling.SetActive(true);
        else m_Bling.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();

    }

    public void f_UpdateWardobeData(Action p_Func = null) {
        string t_BoatData ="";
        string t_RodData = "";

        for (int i = 0; i < m_BoatData.Count; i++) {
            if (m_BoatData[i].m_Locked) t_BoatData += "0";
            else t_BoatData += "1";
        }

        for (int i = 0; i < m_RodData.Count; i++) {
            if (m_RodData[i].m_Locked) t_RodData += "0";
            else t_RodData += "1";
        }

        Debug.Log(t_BoatData);
        Debug.Log(t_RodData);

        PlayerData_Manager.m_Instance.f_UpdatePlayerAvatarList(t_BoatData, t_RodData,p_Func);
    }
}
