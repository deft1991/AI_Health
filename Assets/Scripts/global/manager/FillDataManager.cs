using UnityEngine;
using UnityEngine.SceneManagement;

namespace global
{
    public class FillDataManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        
        public void Startup()
        {
            Debug.Log("Mission manager starting...");
            
            Status = ManagerStatus.Started;
            
            Debug.Log("FillDataManager: started");
        }

        public void LoadFillDataScene()
        {
            SceneManager.LoadScene("MainAppScene");
        }
    }
}