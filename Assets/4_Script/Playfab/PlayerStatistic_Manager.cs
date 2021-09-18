using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerStatistic_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static PlayerStatistic_Manager m_Instance;
    //===== STRUCT =====

    [System.Serializable]
    public class c_ListStatistic
    {
        public List<StatisticUpdate> Statistics;
    }

    public c_ListStatistic m_ListStatistic;
    //===== PUBLIC =====
    public Action m_OnGetDone;
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
    
    public void f_UpdatePlayerStatistics() {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest {
            Statistics = m_ListStatistic.Statistics
        }, f_OnUpdateStatisticSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_OnUpdateStatisticSuccess(UpdatePlayerStatisticsResult p_Result) {
        f_GetPlayerStatistic();
    }

    public void f_GetPlayerStatistic(Action p_Function = null) {
        m_OnGetDone += p_Function;
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest {
        }, f_OnGetStatisticsSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_OnGetStatisticsSuccess(GetPlayerStatisticsResult p_Result) {
        m_ListStatistic = JsonUtility.FromJson<c_ListStatistic>(p_Result.ToJson());

        foreach (StatisticUpdate t_Stats in m_ListStatistic.Statistics) {
            switch (t_Stats.StatisticName){
                case "MultiplierLevel":
                    Player_Manager.m_Instance.m_MultiplierLevel = t_Stats.Value;
                    break;
                case "MultiplierProgress":
                    Player_Manager.m_Instance.m_MultiplierProgress = t_Stats.Value;
                    break;
                case "Score":
                    GameManager_Manager.m_Instance.f_SetHighScore(t_Stats.Value);
                    break;
            }
        }

        m_OnGetDone?.Invoke();
        m_OnGetDone = null;
    }

    public void f_UpdateStatistics(string p_Name,int p_Value) {
        for (int i = 0; i < m_ListStatistic.Statistics.Count; i++) {
            if (m_ListStatistic.Statistics[i].StatisticName == p_Name) {
                m_ListStatistic.Statistics[i].Value = p_Value;
            }
        }
        f_UpdatePlayerStatistics();
    }

    public void f_NewPlayer(Action p_Function = null) {
        m_ListStatistic.Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "MultiplierLevel", Value = 1},
            new StatisticUpdate { StatisticName = "MultiplierProgress", Value = 0},
            new StatisticUpdate { StatisticName = "Score", Value = 0},
        };

        m_OnGetDone += p_Function;
        f_UpdatePlayerStatistics();
    }
}
