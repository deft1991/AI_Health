// using Firebase;
using UnityEngine;

namespace pushNotification.manager
{
    public class PushNotificationManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        // private FirebaseApp _app;
        
        public void Startup()
        {
            Debug.Log("PushNotificationManager starting...");
            Status = ManagerStatus.Initializing;
            
            // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            //     var dependencyStatus = task.Result;
            //     if (dependencyStatus == Firebase.DependencyStatus.Available) {
            //         // Create and hold a reference to your FirebaseApp,
            //         // where app is a Firebase.FirebaseApp property of your application class.
            //         _app = Firebase.FirebaseApp.DefaultInstance;
            //
            //         // Set a flag here to indicate whether Firebase is ready to use by your app.
            //     } else {
            //         UnityEngine.Debug.LogError(System.String.Format(
            //             "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            //         // Firebase Unity SDK is not safe to use here.
            //     }
            // });
            //
            // Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            // Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            
            Status = ManagerStatus.Started;
            Debug.Log("PushNotificationManager: started");
        }
        
        // public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
        //     Debug.Log("Received Registration Token: " + token.Token);
        // }
        //
        // public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
        //     Debug.Log("Received a new message from: " + e.Message.From);
        // }
    }
}