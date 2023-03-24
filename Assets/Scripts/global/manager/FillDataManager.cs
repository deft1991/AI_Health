using UnityEngine;
using UnityEngine.SceneManagement;

namespace global.manager
{
    public class FillDataManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        
        public void Startup()
        {
            Debug.Log("FillDataManager starting...");
            
            Status = ManagerStatus.Started;
            
            Debug.Log("FillDataManager: started");
        }

        public void LoadFillDataScene()
        {
            SceneManager.LoadScene("MainAppScene");
        }
    }
}