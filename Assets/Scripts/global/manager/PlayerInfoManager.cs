using System.Collections.Generic;
using data;
using UnityEngine;

namespace global.manager
{
    public class PlayerInfoManager : MonoBehaviour, IGameManager
    {
        
        public static string WORKOUT_PROGRAM_RECOMMENDATION = "WORKOUT_PROGRAM_RECOMMENDATION";
        public static string NUTRITION_PROGRAM_RECOMMENDATION = "NUTRITION_PROGRAM_RECOMMENDATION";
        
        private static string PLAYER_KEY = "player";
        public ManagerStatus Status { get; private set; }
        public PlayerIfoDto Player { get; private set; }

        public HashSet<MuscleGroupType> WorkoutPrograms { get; set; }
        public WorkoutDifficultyLevelType WorkoutDifficultyLevel { get; set; }
        public int WorkoutDuration { get; set; }

        public void Startup()
        {
            Debug.Log("PlayerInfoManager starting...");


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