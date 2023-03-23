using System.Collections.Generic;
using data;
using UnityEngine;

namespace global
{
    public class MealInfoManager : MonoBehaviour, IGameManager
    {
        
        public static string MEAL_HISTORY = "MEAL_HISTORY";
        public static string CURRENT_MEAL = "MEAL_CURRENT";
        
        public ManagerStatus Status { get; private set; }
        public MealDto Meal { get; private set; }

        public void Startup()
        {
            Debug.Log("MealInfoManager starting...");

            Status = ManagerStatus.Initializing;
            
            Meal = new MealDto();
            // todo maybe add meal history here
            
            Status = ManagerStatus.Started;
            Debug.Log("MealInfoManager: started");
        }
        
    }
}