using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Enumuration {
    public enum e_GameState { 
        Normal = 0,
        Fever = 1,
        NotPlaying =2,
        AfterFever=-1,
        Continue = -2,
    }

    public enum e_PauseState { 
        NotPaused = 0,
        Paused = 1
    }

    public enum e_GoalMovement { 
        Stop = -1,
        Left = 0,
        Right =1
    }

    public enum e_PlayerMovement {
        Stop = -1,
        Left = 0,
        Right = 1
    }
    public enum e_Mute { 
        NotMute = 0,
        Mute = 1
    }
}