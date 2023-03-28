using System;
using System.Collections.Generic;
using data;
using global;
using Newtonsoft.Json;
using nutritionProgram.history.data;
using nutritionProgram.history.@event;
using nutritionProgram.service;
using playerChangeValue.util;
using UnityEngine;
using UnityEngine.UI;

namespace nutritionProgram.history.controller
{
    public class DeleteNutritionProgramHistoryController : MonoBehaviour
    {

        #region PRIVATE

        private void Awake()
        {
            Messenger<string>.AddListener(NutritionProgramHistoryEvent.DELETE_HISTORY, OnDeleteHistory);
        }

        private void OnDestroy()
        {
            Messenger<string>.RemoveListener(NutritionProgramHistoryEvent.DELETE_HISTORY, OnDeleteHistory);
        }

        private void OnDeleteHistory(string historyName)
        {
            string history = PlayerPrefs.GetString(NutritionProgramHistoryManager.NUTRITION_PROGRAM_RECOMMENDATION_HISTORY);
            NutritionProgramHistoryListDto historyListDto = JsonConvert.DeserializeObject<NutritionProgramHistoryListDto>(history);
            if (historyListDto.nutritionPrograms.ContainsKey(historyName))
            {
                historyListDto.nutritionPrograms.Remove(historyName);
            }
            
            string json = JsonConvert.SerializeObject(historyListDto);
            PlayerPrefs.SetString(NutritionProgramHistoryManager.NUTRITION_PROGRAM_RECOMMENDATION_HISTORY, json);
            
            Dictionary<string,NutritionProgramHistoryDto> nutritionPrograms = Managers.NutritionProgramHistoryManager.HistoryListDto.nutritionPrograms;
            if (nutritionPrograms.ContainsKey(historyName))
            {
                nutritionPrograms.Remove(historyName);
            }
        }

        #endregion

        #region PUBLIC
        
        #endregion
    }
}