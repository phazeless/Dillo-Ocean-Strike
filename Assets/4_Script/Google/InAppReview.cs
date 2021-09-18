using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppReview_GameObject : MonoBehaviour
{ 
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static InAppReview_GameObject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====
    public ReviewManager _reviewManager;
    public PlayReviewInfo _playReviewInfo;
    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }

    void Start() {
        StartCoroutine(f_RequestAppReview());
    }

    void Update() {

    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================

    public IEnumerator f_RequestAppReview() {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError) {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError) {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
}
