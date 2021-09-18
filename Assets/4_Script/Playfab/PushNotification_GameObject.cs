using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class PushNotification_GameObject : MonoBehaviour
{
    //=====================================================================
    //				      VARIABLES 
    //=====================================================================
    //===== SINGLETON =====
    public static PushNotification_GameObject m_Instance;
    //===== STRUCT =====
    //===== PUBLIC =====

    //===== PRIVATES =====

    //=====================================================================
    //				MONOBEHAVIOUR METHOD 
    //=====================================================================
    void Awake() {
        m_Instance = this;
    }

    void Start() {
        AndroidNotificationChannel c = new AndroidNotificationChannel() {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        AndroidNotification notification = new AndroidNotification() {
            Title = "SomeTitle",
            Text = "SomeText",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = System.DateTime.Now();
        };
        notification.Title = "SomeTitle";
        notification.Text = "SomeText";
        notification.FireTime = System.DateTime.Now.AddMinutes(5);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    void Update() {

    }
    //=====================================================================
    //				    OTHER METHOD
    //=====================================================================
      
}
