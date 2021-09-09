using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using MEC;
public class Winning_Manager : MonoBehaviour {
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static Winning_Manager m_Instance;
    //===== STRUCT =====

    //===== PUBLIC =====
    public TextMeshProUGUI m_ScoreText;
    public TextMeshProUGUI m_CurrencyText;
    public Button m_DoubleWinningButton;
    public Button m_RetryButton;
    public Button m_HomeButton;
    public AudioSource m_LoppAudio;
    public Button m_MultiplierBackButton;
    public AudioClip m_MultiplierFillUpSound;
    public AudioClip m_MultiplierLevelUpSound;
    public Image m_MultiplierBar;
    public TextMeshProUGUI m_PostMultiplierLevelText;
    public TextMeshProUGUI m_PostMultiplierProgressText;
    public TextMeshProUGUI m_PostMultiplierMaxText;
    //===== PRIVATES =====
    double t_CurrentAmountAnimation = 0;
    double t_NominalPerSecond = 0;
    float t_CurrentProgress;
    double t_Target;

    double t_Score = 0;
    double t_Currency = 0;
    double t_MultiplierProgress = 0;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }

    void Start() {

    }

    void Update() {

    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_StartCounting() {
        m_DoubleWinningButton.interactable = false;
        m_RetryButton.interactable = false;
        m_HomeButton.interactable = false;
        Timing.RunCoroutine(f_CountScore());
    }

    public void f_CountMultiplier() {
        t_Score = 0;
        t_Currency = 0;
        m_ScoreText.text = t_Score.ToString("0000");
        m_CurrencyText.text = "+" + t_Currency.ToString("00");
        m_PostMultiplierLevelText.text = Player_Manager.m_Instance.m_MultiplierLevel.ToString("000");
        m_PostMultiplierProgressText.text = Player_Manager.m_Instance.m_MultiplierProgress.ToString("0000");
        m_PostMultiplierMaxText.text = Player_Manager.m_Instance.m_MaxMultiplierProgress.ToString("0000");
        t_CurrentProgress = (float)(Player_Manager.m_Instance.m_MultiplierProgress / Player_Manager.m_Instance.m_MaxMultiplierProgress);
        m_MultiplierBar.fillAmount = t_CurrentProgress;
        t_MultiplierProgress = Player_Manager.m_Instance.m_MultiplierProgress;
        Timing.RunCoroutine(ie_CountMultiplier());
    }

    public IEnumerator<float> ie_CountMultiplier() {
        Audio_Manager.m_Instance.f_PlayOneShot(m_MultiplierFillUpSound);
        m_MultiplierBackButton.interactable = false;
        yield return Timing.WaitForSeconds(1f);
        t_Target = GameManager_Manager.m_Instance.m_Score;
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(ie_UpdateText(t_MultiplierProgress,t_MultiplierProgress+GameManager_Manager.m_Instance.m_Score,f_UpdateNominal, t_Target /100)));
        m_MultiplierBackButton.interactable = true;
    }

    public IEnumerator<float> f_CountScore() {
        GameManager_Manager.m_Instance.m_Score *= Player_Manager.m_Instance.m_MultiplierLevel;
        GameManager_Manager.m_Instance.f_CheckNewHighscore();
        yield return Timing.WaitForSeconds(1f);
        m_LoppAudio.Play();
        t_Target = GameManager_Manager.m_Instance.m_Score;
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(ie_UpdateText(0,GameManager_Manager.m_Instance.m_Score,f_UpdateScore, t_Target/100)));
        t_Target = (double) Mathf.Ceil(((float)GameManager_Manager.m_Instance.m_Score) / 100);
        m_LoppAudio.Play();
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(ie_UpdateText(0, t_Target, f_UpdateWinnings,t_Target /100)));
        m_DoubleWinningButton.interactable = true;
        m_RetryButton.interactable = true;
        m_HomeButton.interactable = true;
    }

    public void f_UpdateScore(double p_ScoreAdd) {
        t_Score += p_ScoreAdd;
        m_ScoreText.text = t_Score.ToString("0000");
    }

    public void f_UpdateNominal(double p_ProgressAdd) {
        t_MultiplierProgress += p_ProgressAdd;
        Player_Manager.m_Instance.m_MultiplierProgress += p_ProgressAdd;
        if (Player_Manager.m_Instance.m_MultiplierProgress>= Player_Manager.m_Instance.m_MaxMultiplierProgress) {
            Player_Manager.m_Instance.m_MultiplierProgress-= Player_Manager.m_Instance.m_MaxMultiplierProgress;
            Player_Manager.m_Instance.m_MultiplierLevel++;
            Audio_Manager.m_Instance.f_PlayOneShot(m_MultiplierLevelUpSound);
        }
        m_PostMultiplierLevelText.text = Player_Manager.m_Instance.m_MultiplierLevel.ToString("000");
        m_PostMultiplierProgressText.text = Player_Manager.m_Instance.m_MultiplierProgress.ToString("0000");
        m_PostMultiplierMaxText.text = Player_Manager.m_Instance.m_MaxMultiplierProgress.ToString("0000");
        t_CurrentProgress =(float) (Player_Manager.m_Instance.m_MultiplierProgress / Player_Manager.m_Instance.m_MaxMultiplierProgress);
        m_MultiplierBar.fillAmount = t_CurrentProgress;
    }

    public void f_DoubleWinning() {
        m_DoubleWinningButton.interactable = false;
        m_RetryButton.interactable = false;
        m_HomeButton.interactable = false;
        Timing.RunCoroutine(ie_DoubleWinning());
    }

    public IEnumerator<float> ie_DoubleWinning() {
        m_LoppAudio.Play();
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(ie_UpdateText(t_Currency, t_Currency*2, f_UpdateWinnings, (float) (t_Currency)/100)));
        m_RetryButton.interactable = true;
        m_HomeButton.interactable = true;
    }

    public void f_UpdateWinnings(double p_ScoreAdd) {
        Player_Manager.m_Instance.m_Currency += p_ScoreAdd;
        t_Currency += p_ScoreAdd;
        m_CurrencyText.text = "+"+t_Currency.ToString("00");
    }

    public IEnumerator<float> ie_UpdateText(double p_IntialAmount, double p_WinningAmount, Action<double> p_Callback, double p_MoneyPerSecond) {
        t_CurrentAmountAnimation = p_IntialAmount;
        t_NominalPerSecond = p_MoneyPerSecond;
        for (int i = 0; i < 100; i++) {
            p_Callback(t_NominalPerSecond);
            t_CurrentAmountAnimation += t_NominalPerSecond;
            yield return Timing.WaitForOneFrame;
        }
    }
}
