using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumuration;
using MEC;
using EZCameraShake;
public class GameManager_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static GameManager_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public e_GameState m_GameState;
    public e_PauseState m_PauseState;
    public int m_Score;
    public float m_MaxFeverPower = 100f;
    public float m_TapCooldown = 1f;
    public bool m_ContinueChance = true;
    public float m_ContinueCountdown;
    [Header("Score Check Distance")]
    public float m_PerfectDist;
    public float m_GoodDist;
    public float m_BadDist;
    [Header("UI")]
    public GameObject m_ScoreGameobject;
    public TextMeshProUGUI m_ScoreText;
    public TextMeshProUGUI m_HighscoreText;
    public Image m_FeverFill;
    public Image m_NormalFill;
    public Image m_ContinueFill;
    public GameObject m_Countdown;
    public GameObject m_PauseButton;
    public GameObject m_Gameover;
    public GameObject m_Fever;
    public GameObject m_FeverBorder;
    public GameObject m_NormalMode;
    public GameObject m_MenuScreen;
    public GameObject m_EndScreen;
    public GameObject m_PlayBar;
    public GameObject m_FeverBar;
    public GameObject m_PlayArea;
    public GameObject m_ContinueMenu;
    public GameObject m_Dim;
    public Animator m_ResultAnimator;
    [Header("Prefab Spawn")]
    public GameObject m_TextprefabSpawn;
    public GameObject m_FeedbackContainer;
    [Header("Audio")]
    public AudioClip m_PtAcquired;
    public AudioClip m_NewHighScore;
    public AudioClip m_FinishGame;
    //===== PRIVATES =====
    private int m_CurrCombo = 0;
    private float m_CurrFeverPower = 0;
    private float m_TotalDist;
    private float m_TapFeverCooldown;
    private float m_HighScore;
    float t_ContinueTimer;
    float t_Distance;
    Vector3 t_ConvertedPosition = Vector3.zero;
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        m_TotalDist = Goal_Gameobject.m_Instance.m_Radius;
        m_PerfectDist = .25f * m_TotalDist;
        m_GoodDist = .7f * m_TotalDist;
        m_BadDist = 1.1f * m_TotalDist;
    }

    void Update(){
        if (m_GameState == e_GameState.Fever) {
            m_CurrFeverPower -= 20* Time.deltaTime;
            if (m_CurrFeverPower <= 0) {
                m_CurrFeverPower = 0;
                m_Fever.gameObject.SetActive(false);
                m_FeverBorder.gameObject.SetActive(false);
                m_GameState = e_GameState.AfterFever;
                m_NormalMode.SetActive(true);
                Enviorment_Manager.m_Instance.f_BackToNormal();
                Debug.Log("Coroutine");
                Timing.RunCoroutine(ie_AfterFever());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) f_Tap();

        if (m_GameState == e_GameState.AfterFever && m_TapFeverCooldown > 0) {
            m_TapFeverCooldown -= Time.deltaTime;
            if (m_TapFeverCooldown <= 0) m_TapFeverCooldown = 0;
        }

        if (m_GameState == e_GameState.Continue) {
            t_ContinueTimer -= Time.deltaTime;
            m_ContinueFill.fillAmount = (m_ContinueCountdown-t_ContinueTimer) / m_ContinueCountdown;
            if (t_ContinueTimer <= 0) {
                t_ContinueTimer = 0;
                f_EndGame();
            }
        }

        m_ScoreText.text = m_Score.ToString("0000");
        m_FeverFill.fillAmount = m_CurrFeverPower / m_MaxFeverPower;
        m_NormalFill.fillAmount = m_CurrFeverPower / m_MaxFeverPower;
    }

    private void OnDrawGizmosSelected() {

    }

    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_StartGame() {
        m_Countdown.gameObject.SetActive(true);
        Enviorment_Manager.m_Instance.f_BackToNormal();
        Character_Gameobject.m_Instance.f_Reset();

        Player_Gameobject.m_Instance.f_Init();
        Goal_Gameobject.m_Instance.f_Init();

        Player_Gameobject.m_Instance.gameObject.SetActive(true);
        Goal_Gameobject.m_Instance.gameObject.SetActive(true);

        m_PlayBar.SetActive(true);
        m_FeverBar.SetActive(true);

        m_Score = 0;
        m_CurrFeverPower = 0;
        m_CurrCombo = 0;
        m_ContinueChance = true;
    }
    public void f_Tap() {
        if (m_GameState != e_GameState.AfterFever || (m_GameState == e_GameState.AfterFever && m_TapFeverCooldown <= 0)) {
            f_CompareGoalAndPlayer();
            if (m_GameState == e_GameState.AfterFever) m_TapFeverCooldown = m_TapCooldown;
        }
    }

    public void f_CompareGoalAndPlayer() {
        t_ConvertedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        t_ConvertedPosition.z = Camera.main.nearClipPlane;
        t_Distance = Mathf.Abs(Player_Gameobject.m_Instance.transform.position.x - Goal_Gameobject.m_Instance.transform.position.x);

        if (m_GameState == e_GameState.Normal || m_GameState == e_GameState.AfterFever) {

            if (t_Distance < m_PerfectDist) {
                m_Score += 25;
                if (m_GameState != e_GameState.AfterFever) m_CurrFeverPower += 10;
                m_CurrCombo++;
                Character_Gameobject.m_Instance.f_Pull(false,2);
                TapEffectPool_Manager.m_Instance.f_SpawnTap(0,t_ConvertedPosition);
                Player_Gameobject.m_Instance.f_Tap();
                Goal_Gameobject.m_Instance.f_OnTap();
                Combo_Manager.m_Instance.f_SpawnPoint(0);
                TapFx_Manager.m_Instance.f_SpawnPoint(0);
                ComboText_Manager.m_Instance.f_SpawnPoint(m_CurrCombo);
                Audio_Manager.m_Instance.f_PlayOneShot(m_PtAcquired);
                CameraShaker.Instance.ShakeOnce(2.0f,2.0f,.1f,.1f);
            }
            else if (t_Distance < m_GoodDist) {
                m_Score += 15;
                if (m_GameState != e_GameState.AfterFever) m_CurrFeverPower += 5;
                m_CurrCombo++;
                Character_Gameobject.m_Instance.f_Pull(false,1);
                TapEffectPool_Manager.m_Instance.f_SpawnTap(1, t_ConvertedPosition);
                Player_Gameobject.m_Instance.f_Tap();
                Goal_Gameobject.m_Instance.f_OnTap();
                Combo_Manager.m_Instance.f_SpawnPoint(1);
                TapFx_Manager.m_Instance.f_SpawnPoint(1);
                ComboText_Manager.m_Instance.f_SpawnPoint(m_CurrCombo);
                Audio_Manager.m_Instance.f_PlayOneShot(m_PtAcquired);
            }
            else if (t_Distance < m_BadDist) {
                m_Score += 5;
                if (m_GameState != e_GameState.AfterFever) m_CurrFeverPower += 1;
                m_CurrCombo = 0;
                Character_Gameobject.m_Instance.f_Pull(false,0);
                TapEffectPool_Manager.m_Instance.f_SpawnTap(2, t_ConvertedPosition);
                Player_Gameobject.m_Instance.f_Tap();
                Goal_Gameobject.m_Instance.f_OnTap();
                Combo_Manager.m_Instance.f_SpawnPoint(2);
                TapFx_Manager.m_Instance.f_SpawnPoint(2);
                Audio_Manager.m_Instance.f_PlayOneShot(m_PtAcquired);
            }
            else {
                if (m_GameState != e_GameState.AfterFever) {
                    TapEffectPool_Manager.m_Instance.f_SpawnTap(3, t_ConvertedPosition);
                    Character_Gameobject.m_Instance.f_Pull(true);
                    m_Gameover.gameObject.SetActive(true);
                    Combo_Manager.m_Instance.f_SpawnPoint(4);
                    m_CurrCombo = 0;
                    f_Miss();
                }
            }

            if (m_CurrFeverPower >= m_MaxFeverPower) {
                m_CurrFeverPower = m_MaxFeverPower;
                m_Fever.gameObject.SetActive(true);
                m_FeverBorder.gameObject.SetActive(true);
                m_FeverBar.gameObject.SetActive(true);
                m_NormalMode.SetActive(false);
                m_GameState = e_GameState.Fever;
                Enviorment_Manager.m_Instance.f_TransitionToStorm();
            }
        }
        else if (m_GameState == e_GameState.Fever ) {
                m_Score += 25;
                m_CurrCombo++;
                //PointText_Manager.m_Instance.f_SpawnPoint(25, m_TextprefabSpawn.transform.position);
                Character_Gameobject.m_Instance.f_Pull(false,3);
                TapEffectPool_Manager.m_Instance.f_SpawnTap(0, t_ConvertedPosition);
                Combo_Manager.m_Instance.f_SpawnPoint(0);
                TapFx_Manager.m_Instance.f_SpawnPoint(0);
                ComboText_Manager.m_Instance.f_SpawnPoint(m_CurrCombo);
                Audio_Manager.m_Instance.f_PlayOneShot(m_PtAcquired);
                CameraShaker.Instance.ShakeOnce(2.0f, 2.0f, .1f, .1f);
        }
    }

    public bool f_IsGamePlaying() {
        if (m_PauseState == e_PauseState.Paused) return false;
        else {
            if (m_GameState == e_GameState.NotPlaying || m_GameState == e_GameState.Continue) return false;
            else return true;
        }
    }

    public bool f_IsGameFever() {
        if (m_GameState != e_GameState.Fever) return false;
        else return true;
    }

    public void f_PauseGame() {
        Time.timeScale = 0;
        m_PlayArea.SetActive(false);
        m_PauseState = e_PauseState.Paused;
    }

    public void f_Unpause() {
        Time.timeScale = 1;
        m_PlayArea.SetActive(true);
        m_PauseState = e_PauseState.NotPaused;
    }

    public void f_SetHighScore(int p_Value) {
        m_HighScore = p_Value;
    }
    public void f_ChangeFeverMode() {
        if (!f_IsGameFever()) {
            m_CurrFeverPower = 999999999999999999f;
            m_Fever.gameObject.SetActive(true);
            m_FeverBorder.gameObject.SetActive(true);
            m_NormalMode.SetActive(false);
            m_GameState = e_GameState.Fever;
            Enviorment_Manager.m_Instance.f_TransitionToStorm();
        }
        else {
            m_CurrFeverPower = 0;
            m_Fever.gameObject.SetActive(false);
            m_FeverBorder.gameObject.SetActive(false);
            m_GameState = e_GameState.Normal;
            m_NormalMode.SetActive(true);
            Enviorment_Manager.m_Instance.f_BackToNormal();
        }
    }

    public void f_Miss() {
        Player_Gameobject.m_Instance.gameObject.SetActive(false);
        Goal_Gameobject.m_Instance.gameObject.SetActive(false);
        m_GameState = e_GameState.NotPlaying;

        m_ScoreGameobject.gameObject.SetActive(false);
        m_PauseButton.gameObject.SetActive(false);
        m_FeverBar.SetActive(false);
        m_PlayBar.SetActive(false);
        m_PlayArea.SetActive(false);
    }

    public void f_EndGame() {
        f_Miss();

        if (m_ContinueChance) {
            Debug.Log("Test");
            m_ContinueChance = false;
            t_ContinueTimer = m_ContinueCountdown;
            m_GameState = e_GameState.Continue;
            m_ContinueMenu.gameObject.SetActive(true);
        }
        else {
            m_Dim.SetActive(true);
            m_GameState = e_GameState.NotPlaying;
            t_ContinueTimer = 0;
            m_ContinueMenu.gameObject.SetActive(false);
            m_Gameover.gameObject.SetActive(false);
            f_PostGame();
        }
    }

    public void f_Continue() {
        m_Gameover.gameObject.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();
        Player_Gameobject.m_Instance.gameObject.SetActive(true);
        Goal_Gameobject.m_Instance.gameObject.SetActive(true);
        m_ScoreGameobject.gameObject.SetActive(true);
        m_PauseButton.gameObject.SetActive(true);
        m_FeverBar.SetActive(true);
        m_PlayBar.SetActive(true);
        m_PlayArea.SetActive(true);
        m_ContinueMenu.gameObject.SetActive(false);
        m_GameState = e_GameState.Normal;
    }

    public void f_PostGame() {
        m_Score = Mathf.RoundToInt( m_Score* Boat_Gameobject.m_Instance.m_ScoreMultiplier);
        Winning_Manager.m_Instance.f_CountMultiplier();
        Audio_Manager.m_Instance.f_PlayOneShot(m_FinishGame);
        m_EndScreen.gameObject.SetActive(true);
    }

    public void f_CheckNewHighscore() {
        if (m_Score > m_HighScore) {
            m_HighScore = m_Score;
            m_ResultAnimator.SetBool("Highscore", true);
            Audio_Manager.m_Instance.f_PlayOneShot(m_NewHighScore);
            PlayerStatistic_Manager.m_Instance.f_UpdateStatistics("Score",(int)m_HighScore);
        }
        else m_ResultAnimator.SetBool("Highscore", false);
    }


    public void f_BackToHome() {
        m_GameState = e_GameState.NotPlaying;
        m_PauseButton.gameObject.SetActive(false);
        Player_Gameobject.m_Instance.gameObject.SetActive(false);
        Goal_Gameobject.m_Instance.gameObject.SetActive(false);
        m_PlayBar.gameObject.SetActive(false);
        m_FeverBar.SetActive(false);
        Character_Gameobject.m_Instance.f_Reset();

        m_ScoreGameobject.gameObject.SetActive(false);
        m_FeverBorder.gameObject.SetActive(false);
        m_Fever.gameObject.SetActive(false);
        m_NormalMode.SetActive(true);
        Enviorment_Manager.m_Instance.f_BackToNormal();
        LeaderboardManager_Manager.m_Instance.f_GetLeaderBoard();
        f_Unpause();
        m_PlayArea.SetActive(false);
    }

    public void f_Start() {
        m_ScoreGameobject.gameObject.SetActive(true);
        m_PauseButton.gameObject.SetActive(true);
        m_PlayArea.SetActive(true);
        m_GameState = e_GameState.Normal;
    }

    IEnumerator<float> ie_AfterFever() {
        yield return Timing.WaitForSeconds(2.5f);
        if (m_GameState == e_GameState.AfterFever) m_GameState = e_GameState.Normal;
    }
}
