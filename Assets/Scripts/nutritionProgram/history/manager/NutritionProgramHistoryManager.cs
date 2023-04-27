using System;
using global;
using Newtonsoft.Json;
using nutritionProgram.history.data;
using nutritionProgram.history.@event;
using UnityEngine;

namespace nutritionProgram.service
{
    public class    NutritionProgramHistoryManager : MonoBehaviour, IGameManager
    {
        
        public static string NUTRITION_PROGRAM_RECOMMENDATION_HISTORY = "NUTRITION_PROGRAM_RECOMMENDATION_HISTORY";
        
        public NutritionProgramHistoryListDto HistoryListDto { get; private set;}
        
        public ManagerStatus Status { get; private set; }
        
        public void Startup()
        {
            Debug.Log("NutritionProgramHistoryManager starting...");
            Status = ManagerStatus.Initializing;
            
            Messenger<string, string>.AddListener(NutritionProgramHistoryEvent.SAVE_HISTORY, OnSaveHistory);
            // todo add remove history listener
            
            // load history
            HistoryListDto = OnGetHistory();
            
            Status = ManagerStatus.Started;
            Debug.Log("NutritionProgramHistoryManager: started");
        }

        private void OnDestroy()
        {
            Messenger<string, string>.RemoveListener(NutritionProgramHistoryEvent.SAVE_HISTORY, OnSaveHistory);
        }

        private void OnSaveHistory(string programName, string nutritionProgram)
        {
            string history = PlayerPrefs.GetString(NUTRITION_PROGRAM_RECOMMENDATION_HISTORY);
            NutritionProgramHistoryListDto historyListDto = new NutritionProgramHistoryListDto();
            if (!String.IsNullOrEmpty(history))
            {
                historyListDto = JsonConvert.DeserializeObject<NutritionProgramHistoryListDto>(history);
            }
            
            NutritionProgramHistoryDto dto = new NutritionProgramHistoryDto()
            {
                programName = programName,
                nutritionProgram = nutritionProgram,
                Goal = Managers.PlayerInfoManager.Player.goal,
                generateTime = DateTime.Now
            };
            
            /*
             * add it into DTO for save
             */
            historyListDto.nutritionPrograms.Add(programName, dto);
            
            /*
             * add it into Manager object to show
             */
            this.HistoryListDto.nutritionPrograms.Add(programName, dto);

            string json = JsonConvert.SerializeObject(historyListDto);
            PlayerPrefs.SetString(NUTRITION_PROGRAM_RECOMMENDATION_HISTORY, json);
        }

        private NutritionProgramHistoryListDto OnGetHistory()
        {
            // todo add sort by goal
            string history = PlayerPrefs.GetString(NUTRITION_PROGRAM_RECOMMENDATION_HISTORY);
            NutritionProgramHistoryListDto historyListDto = new NutritionProgramHistoryListDto();
            if (!String.IsNullOrEmpty(history))
            {
                historyListDto = JsonConvert.DeserializeObject<NutritionProgramHistoryListDto>(history);
            }

            return historyListDto;
        }
    }
}