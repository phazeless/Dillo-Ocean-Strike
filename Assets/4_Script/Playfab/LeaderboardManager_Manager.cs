using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardManager_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static LeaderboardManager_Manager m_Instance;
    //===== STRUCT =====
    [System.Serializable]
    public class c_LeaderboardList
    {
        public c_LeaderboardData[] Leaderboard;
        public int version;
    }
    [System.Serializable]
    public class c_LeaderboardData
    {
        public string PlayFabId;
        public string DisplayName;
        public string StatValue;
        public string Position;
        public c_Profile Profile;
    }
    [System.Serializable]
    public class c_Profile
    {
        public string PublisherId;
        public string TitleId;
        public string PlayerId;
        public string DisplayName;
    }
    //===== PUBLIC =====
    public c_LeaderboardList m_LeaderboardList;
    public c_LeaderboardList m_PlayerLeaderboard;
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

    /// <summary>
    /// Method to GetLeaderboard from PlayFabAPI
    /// </summary>
    /// <param name="p_StatisticName">The Heroes Name you want to see the leaderboard, Default = "First Hero"</param>
    public void f_GetLeaderBoard(Action p_Func = null,string p_StatisticName = "Score") {
        LeaderboardPool_Manager.m_Instance.f_DeactivateAll();
        m_OnGetDone += p_Func;
        //TODO : Nanti parameter defaultnya ganti ke hero pertama yang bakal dikeluarin saat mencet leaderboard
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest {
                StartPosition = 0,
                MaxResultsCount = 10,
                StatisticName = p_StatisticName
            }, OnGetLeaderboardSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_GetPlayerLeaderBoard(string p_StatisticName = "Score") {
        PlayFabClientAPI.GetLeaderboardAroundPlayer(
            new GetLeaderboardAroundPlayerRequest {
                MaxResultsCount = 1,
                StatisticName = p_StatisticName
            }, OnGetLeaderboardPlayerSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    /// <summary>
    /// Method that called if get leaderboard request return succeess
    /// </summary>
    /// <param name="p_Result">result details from request</param>
    public void OnGetLeaderboardSuccess(GetLeaderboardResult p_Result) {
        m_LeaderboardList = JsonUtility.FromJson<c_LeaderboardList>(p_Result.ToJson());
        for (int i = 0; i < m_LeaderboardList.Leaderboard.Length; i++) {
            LeaderboardPool_Manager.m_Instance.f_Spawn(m_LeaderboardList.Leaderboard[i].Position, m_LeaderboardList.Leaderboard[i].DisplayName, m_LeaderboardList.Leaderboard[i].StatValue);
        }
        f_GetPlayerLeaderBoard();
    }

    public void OnGetLeaderboardPlayerSuccess(GetLeaderboardAroundPlayerResult p_Result) {
        m_PlayerLeaderboard = JsonUtility.FromJson<c_LeaderboardList>(p_Result.ToJson());
        m_OnGetDone?.Invoke();
        m_OnGetDone = null;
        Player_Manager.m_Instance.f_SetLeadeboard( int.Parse(m_PlayerLeaderboard.Leaderboard[0].Position) + 1, m_PlayerLeaderboard.Leaderboard[0].DisplayName, int.Parse(m_PlayerLeaderboard.Leaderboard[0].StatValue));
        LeaderboardPool_Manager.m_Instance.f_InitPlayer(m_PlayerLeaderboard.Leaderboard[0].Position, m_PlayerLeaderboard.Leaderboard[0].DisplayName, m_PlayerLeaderboard.Leaderboard[0].StatValue);
    }

    public void f_GetLeaderboards() {
        Player_Manager.m_Instance.f_LoadingStart();
        f_GetLeaderBoard(Player_Manager.m_Instance.f_LoadingFinish);
    }
}
