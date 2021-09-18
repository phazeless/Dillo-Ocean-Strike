using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;
using PlayFab;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginManager_Manager : MonoBehaviour{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static LoginManager_Manager m_Instance;
    //===== STRUCT =====
    [System.Serializable]
    public class c_LoginData
    {
        public string SessionTicket;
        public string PlayFabId;
        public string NewlyCreated;
        public c_UserSetting SettingsForUser;
        public string LastLoginTime;
        public c_EntityToken EntityToken;
        public c_TreatmentAssignment TreatmentAssignment;
        public string DisplayName;
    }
    [System.Serializable]
    public class c_EntityToken
    {
        public string EntityToken;
        public string TokenExpiration;
        public c_EntityKey Entity;
    }
    [System.Serializable]
    public class c_EntityKey
    {
        public string Id;
        public string Type;
        public string TypeString;
    }
    [System.Serializable]
    public class c_UserSetting
    {
        public bool NeedsAttribution;
        public bool GatherDeviceInfo;
        public bool GatherFocusInfo;
    }
    [System.Serializable]
    public class c_TreatmentAssignment
    {
        public string[] Variants;
        public string[] Variables;
    }

    //===== PUBLIC =====
    public c_LoginData m_LoginData;
    public bool m_GuestLoggedIn;
    public bool m_IsLinked;
    public TextMeshProUGUI m_Text;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake(){
        m_Instance = this;
    }

    void Start(){
        Debug.Log(ReturnAndroidID());
        f_InitializePlayGamesConfig();
        //f_GoogleSignInRequest();
        //f_GuestLoginRequest();
    }

    void Update(){
        
    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
    /// <summary>
    /// Method that will be called after Guest Login Request, resulted in success
    /// </summary>
    /// <param name="p_Result">Result details from the request</param>
    private void f_OnLoginSuccess(LoginResult p_Result) {
        m_LoginData = JsonUtility.FromJson<c_LoginData>(p_Result.ToJson());

        if (m_LoginData.NewlyCreated == "true") {
            PlayerStatistic_Manager.m_Instance.f_NewPlayer(f_OnGetStatisticsDone);
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = ReturnDeviceName() }, f_OnDisplayNameSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
        }
        else {
            PlayerStatistic_Manager.m_Instance.f_GetPlayerStatistic(f_OnGetStatisticsDone);
        }

        if (p_Result.InfoResultPayload.PlayerProfile != null) {
            m_LoginData.DisplayName = p_Result.InfoResultPayload.PlayerProfile.DisplayName;
        }

      
    }

    /// <summary>
    /// Method for Requesting PlayFabClientAPI a Guest Login
    /// </summary>
    public void f_GuestLoginRequest() {
        Player_Manager.m_Instance.f_LoadingStart();
#if UNITY_ANDROID
        LoginWithAndroidDeviceIDRequest RequestAndroid = new LoginWithAndroidDeviceIDRequest {
            AndroidDeviceId = ReturnAndroidID(),
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(RequestAndroid, f_OnLoginSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
#endif
#if UNITY_STANDALONE_WIN
        LoginWithCustomIDRequest t_Req = new LoginWithCustomIDRequest {
            CustomId = "Testing",
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(t_Req, f_OnLoginSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
#endif
    }

    /// <summary>
    /// Method for requesting unlink Android Device ID to playfab
    /// </summary>
    public void f_UnlinkGuestRequest() {
        PlayFabClientAPI.UnlinkAndroidDeviceID(new UnlinkAndroidDeviceIDRequest {
            AndroidDeviceId = ReturnAndroidID(),
        }, OnUnlinkGuestSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    /// <summary>
    /// Method that will be called after Unlink Android ID from playfab resulted in success
    /// </summary>
    /// <param name="p_Result">Result details from request</param>
    public void OnUnlinkGuestSuccess(UnlinkAndroidDeviceIDResult p_Result) {
       Debug.Log("Success Unlink Guest");
    }

    public void f_OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult p_Result) {
        Debug.Log(p_Result.DisplayName);
    }

    public static string ReturnAndroidID() {
        return SystemInfo.deviceUniqueIdentifier;
    }

    public static string ReturnDeviceName() {
        return SystemInfo.deviceName;
    }

#if UNITY_ANDROID
    #region GOOGLE
    /// <summary>
    /// Method for Initialize Google Play Games Config
    /// </summary>
    public void f_InitializePlayGamesConfig() {
        // The following grants profile access to the Google Play Games SDK.
        // Note: If you also want to capture the player's Google email, be sure to add
        // .RequestEmail() to the PlayGamesClientConfiguration
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .AddOauthScope("profile")
        .RequestServerAuthCode(false)
        .Build();
        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    /// <summary>
    /// Method for Login to Google Request, will be put in button google sign in
    /// </summary>
    public void f_GoogleSignInRequest() {
        Social.localUser.Authenticate((p_Success) => {
            if (p_Success) {
                Player_Manager.m_Instance.f_LoadingStart();
                string t_ServerAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                m_Text.text = t_ServerAuthCode;
                f_LoginWithGoogle(t_ServerAuthCode);
                //if (m_GuestLoggedIn && !m_IsLinked) {
                //    f_LinkWithGoogle(t_ServerAuthCode);
                //}
                //else if (!m_GuestLoggedIn && !m_IsLinked) {
                    
                //}
            }
            else {
                m_Text.text =""+ p_Success;
                Debug.Log(p_Success);
            }

        });
    }

    /// <summary>
    /// Method for Login Playfab with google request
    /// </summary>
    /// <param name="p_ServerAuthCode">Server Authorization Code from google</param>
    public void f_LoginWithGoogle(string p_ServerAuthCode) {
        PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest() {
            TitleId = PlayFabSettings.TitleId,
            ServerAuthCode = p_ServerAuthCode,
            CreateAccount = true
        }, f_OnLoginSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    /// <summary>
    /// Method for Link Playfab with google request
    /// </summary>
    /// <param name="p_ServerAuthCode">Server Authorization Code from google</param>
    public void f_LinkWithGoogle(string p_ServerAuthCode) {
        PlayFabClientAPI.LinkGoogleAccount(new LinkGoogleAccountRequest() {
            ForceLink = true,
            ServerAuthCode = p_ServerAuthCode,
        }, OnPlayfabLinkGoogleSuccess, PlayFab_Error.m_Instance.f_OnPlayFabError);
    }

    /// <summary>
    /// Method that will be called after link playfab with google request return true / succeed
    /// </summary>
    /// <param name="p_Result">result details from request</param>
    public void OnPlayfabLinkGoogleSuccess(LinkGoogleAccountResult p_Result) {
        m_IsLinked = true;
        f_UnlinkGuestRequest();
    }

    #endregion
#endif 

    public void f_OnGetStatisticsDone() {
        CurrencyManager_Manager.m_Instance.f_GetCurrency(f_OnGetCurrencyDone);
    }
    public void f_OnGetCurrencyDone() {
        LeaderboardManager_Manager.m_Instance.f_GetLeaderBoard(f_OnGetLeaderboardDone);
    }
    public void f_OnGetLeaderboardDone() {
        if (m_LoginData.NewlyCreated == "true") {
            Wardobe_Manager.m_Instance.f_UpdateWardobeData();
            PlayerData_Manager.m_Instance.f_UpdatePlayerAvatarData(0, 0);
            f_GetPlayerDataDone();
        }
        else PlayerData_Manager.m_Instance.f_GetPlayerData(f_GetPlayerDataDone);
    }
    public void f_GetPlayerDataDone() {
       Player_Manager.m_Instance.f_LoadingFinish();
       Player_Manager.m_Instance.f_CheckInternetStart();
    }

}
