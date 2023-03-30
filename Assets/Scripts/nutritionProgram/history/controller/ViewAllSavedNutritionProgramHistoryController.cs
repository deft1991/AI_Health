using System;
using data;
using global;
using nutritionProgram.history.data;
using nutritionProgram.history.@event;
using playerChangeValue.util;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace nutritionProgram.history.controller
{
    public class ViewAllSavedNutritionProgramHistoryController : MonoBehaviour
    {
        [SerializeField] private GameObject historyItemPrefab;
        [SerializeField] private VerticalLayoutGroup layout;

        #region PRIVATE

        private void Awake()
        {
            Messenger.AddListener(NutritionProgramHistoryEvent.SHOW_HISTORY, OnOpenHistory);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(NutritionProgramHistoryEvent.SHOW_HISTORY, OnOpenHistory);
        }

        private void Start()
        {
            GridLayoutHelper.ClearLayoutGroup(layout);

            // todo add fill layout
        }

        private void OnOpenHistory()
        {
            GridLayoutHelper.ClearLayoutGroup(layout);
            NutritionProgramHistoryListDto historyListDto = Managers.NutritionProgramHistoryManager.HistoryListDto;
            
            /*
             * Create history lines.  
             */
            foreach (NutritionProgramHistoryDto dto in historyListDto.nutritionPrograms.Values)
            {
                GameObject historyLine = Instantiate(historyItemPrefab, layout.transform, false);
                HistoryItem historyItem = historyLine.GetComponent<HistoryItem>();
                historyItem.SetHistoryValues(
                    dto.programName,
                    PlayerIfoDto.GetGoalString(dto.Goal),
                    dto.generateTime.ToString());
            }
        }

        #endregion

        #region PUBLIC
        
        #endregion
    }
}