using UnityEngine;
using UnityEngine.SceneManagement;

namespace global
{
    public class ChatGPTManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        
        public void Startup()
        {
            Debug.Log("Mission manager starting...");
            
            Status = ManagerStatus.Started;
            Debug.Log("ChatGPTManager: started");
        }

        public void LoadFillDataScene()
        {
            SceneManager.LoadScene("MainAppScene");
        }

        public void GetRecommendation()
        {
            Debug.Log("Get Chat GPT recommendation");
        }
    }
}