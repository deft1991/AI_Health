using data;
using UnityEditor;
using UnityEngine;

namespace global
{
    public class ChatGPTSecurityManager : MonoBehaviour, IGameManager
    {
        private static string AUTH_FILE = "auth.json";

        public ManagerStatus Status { get; private set; }
        public ChatGPTSecurityDto ChatGPTSecurity { get; private set; }

        public void Startup()
        {
            Debug.Log("Mission manager starting...");

            // todo download state from prefs

            //Load text from a JSON file (Assets/auth.json)
            var jsonTextFile = Resources.Load<TextAsset>(AUTH_FILE); 
            
            //Then use JsonUtility.FromJson<T>() to deserialize jsonTextFile into an object
            ChatGPTSecurity = JsonUtility.FromJson<ChatGPTSecurityDto>(jsonTextFile.text);
            
            Debug.Log("ChatGPTSecurityManager: started");
        }
    }
}