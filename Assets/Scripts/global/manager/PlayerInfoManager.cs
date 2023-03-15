using data;
using UnityEngine;

namespace global
{
    public class PlayerInfoManager : MonoBehaviour, IGameManager
    {
        private static string PLAYER_KEY = "player";
        public ManagerStatus Status { get; private set; }
        public PlayerIfoDto Player { get; private set; }


        public void Startup()
        {
            Debug.Log("Mission manager starting...");


            Status = ManagerStatus.Initializing;
            // todo download state from prefs
            if (IsSaveExists())
            {
                Player = JsonUtility.FromJson<PlayerIfoDto>(PlayerPrefs.GetString(PLAYER_KEY));
            }
            else
            {
                Player = new PlayerIfoDto();
            }
            // create player manager

            Status = ManagerStatus.Started;
            Debug.Log("PlayerInfoManager: started");
        }


        public void SavePlayer()
        {
            string value = JsonUtility.ToJson(Player);
            Debug.Log("Save player: " + value);
            PlayerPrefs.SetString(PLAYER_KEY, value);
        }

        private static bool IsSaveExists()
        {
            return PlayerPrefs.HasKey(PLAYER_KEY);
        }
    }
}