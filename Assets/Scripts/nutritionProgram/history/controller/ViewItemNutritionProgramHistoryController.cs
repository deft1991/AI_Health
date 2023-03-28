using System;
using System.Collections.Generic;
using data;
using global;
using Newtonsoft.Json;
using nutritionProgram.history.data;
using nutritionProgram.history.@event;
using nutritionProgram.service;
using playerChangeValue.util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace nutritionProgram.history.controller
{
    public class ViewItemNutritionProgramHistoryController : MonoBehaviour
    {

        [SerializeField] private TMP_Text programName;
        [SerializeField] private string programNameAppender = " Nutrition Program: ";
        [SerializeField] private TMP_Text programRecommendation;
        private string _programName;

        #region PRIVATE

        private void Awake()
        {
            Messenger<string>.AddListener(NutritionProgramHistoryEvent.SHOW_DETAILED_HISTORY, OnShowDetailedHistory);
        }

        private void OnDestroy()
        {
            Messenger<string>.RemoveListener(NutritionProgramHistoryEvent.SHOW_DETAILED_HISTORY, OnShowDetailedHistory);
        }

        private void OnShowDetailedHistory(string historyName)
        {
            _programName = historyName;
            string history = PlayerPrefs.GetString(NutritionProgramHistoryManager.NUTRITION_PROGRAM_RECOMMENDATION_HISTORY);
            NutritionProgramHistoryListDto historyListDto = JsonConvert.DeserializeObject<NutritionProgramHistoryListDto>(history);
            if (historyListDto.nutritionPrograms.ContainsKey(historyName))
            {
                NutritionProgramHistoryDto nutritionProgramHistoryDto = historyListDto.nutritionPrograms[historyName];
                // add here
                programName.text = historyName + programNameAppender;
                programRecommendation.text = nutritionProgramHistoryDto.nutritionProgram;
            }
            
        }

        #endregion

        #region PUBLIC

        public void OnDeleteDetailedHistory()
        {
            Messenger<string>.Broadcast(NutritionProgramHistoryEvent.DELETE_HISTORY, _programName);
            
            /*
             * Set data
             */
            Messenger.Broadcast(NutritionProgramHistoryEvent.SHOW_HISTORY);
            /*
             * Show screen
             */
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_NUTRITION_PROGRAM_HISTORY);
        }
        
        #endregion
    }
}