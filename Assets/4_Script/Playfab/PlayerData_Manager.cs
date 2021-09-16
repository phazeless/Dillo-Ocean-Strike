using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
public class PlayerData_Manager : MonoBehaviour {
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static PlayerData_Manager m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    [System.Serializable]
    public class c_Data : SerializableDictionary<string, c_DataDetails> { }

    [System.Serializable]
    public class c_PlayerDataList {
        public c_Data Data = new c_Data();
        public string DataVersion;
    }

    [System.Serializable]
    public class c_DataDetails {
        public string Value;
        public string LastUpdated;
        public string Permission;
    }

    public c_PlayerDataList m_PlayerDataList;
    public Action m_OnGetDone;
    //===== PRIVATES =====
    const string m_BoattKey = "CLOTHES";
    const string m_RodKey = "PANTS";
    const string m_BoatAvatarListKey = "BoatAvatarList";
    const string m_RodAvatarListKey = "RodAvatarList";
    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    private void Awake() {
        m_Instance = this;
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    public void f_UpdatePlayerAvatarData(int p_BoatID, int p_RodID) {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {m_BoattKey, p_BoatID.ToString()},
                {m_RodKey, p_RodID.ToString()}
            },
            Permission = UserDataPermission.Public
        }, f_OnUpdatePlayerDataSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_UpdatePlayerAvatarList(string p_BoatAvatarList,string p_RodList,Action p_Func=null) {
        m_OnGetDone += p_Func;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {m_BoatAvatarListKey, p_BoatAvatarList},
                {m_RodAvatarListKey, p_RodList }
            },
            Permission = UserDataPermission.Public
        }, f_OnUpdatePlayerDataSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_GetPlayerData(Action p_Func = null) {
        m_OnGetDone += p_Func;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
            PlayFabId = LoginManager_Manager.m_Instance.m_LoginData.PlayFabId,
            Keys = null,
        }, f_OnGetPlayerDataSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    public void f_OnUpdatePlayerDataSuccess(UpdateUserDataResult p_Result) {
        f_GetPlayerData();
    }

    public void f_OnGetPlayerDataSuccess(GetUserDataResult p_Result) {
        m_PlayerDataList = JsonConvert.DeserializeObject<c_PlayerDataList>(p_Result.ToJson());

        c_DataDetails t_BoatList;
        c_DataDetails t_RodList;
        c_DataDetails t_UsedRod;
        c_DataDetails t_UsedBoat;

        m_PlayerDataList.Data.TryGetValue(m_BoattKey, out t_UsedBoat);
        m_PlayerDataList.Data.TryGetValue(m_RodKey, out t_UsedRod);

        if (t_UsedRod != null && t_UsedBoat != null) {
            Wardobe_Manager.m_Instance.f_SetClothes(t_UsedBoat.Value, t_UsedRod.Value);
        }

        m_PlayerDataList.Data.TryGetValue(m_BoatAvatarListKey, out t_BoatList);
        m_PlayerDataList.Data.TryGetValue(m_RodAvatarListKey, out t_RodList);

        if (t_BoatList != null && t_RodList != null) Wardobe_Manager.m_Instance.f_GetWardobeData(t_BoatList.Value, t_RodList.Value);

        m_OnGetDone?.Invoke();
        m_OnGetDone = null;
    }
   
}