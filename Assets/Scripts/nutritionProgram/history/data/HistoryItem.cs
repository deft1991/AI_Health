using System;
using nutritionProgram.history.@event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace data
{
    public class HistoryItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text programNameText;
        [SerializeField] private TMP_Text goalText;
        [SerializeField] private TMP_Text dateText;

        [SerializeField] private Button mainButton;
        [SerializeField] private Button deleteButton;
        // todo add on click function

        #region PRIVATE

        private void Start()
        {
            // deleteButton.onClick.AddListener(OnClickDelete);
            mainButton.onClick.AddListener(OnClickHistory);
        }

        public void OnClickHistory()
        {
            // todo open detailed program info
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_DETAILED_NUTRITION_PROGRAM_ITEM_HISTORY);
            Messenger<string>.Broadcast(NutritionProgramHistoryEvent.SHOW_DETAILED_HISTORY, programNameText.text);
        }

        public void OnClickDelete()
        {
            // todo add delete history later
            Destroy(this.gameObject);
            Messenger<string>.Broadcast(NutritionProgramHistoryEvent.DELETE_HISTORY, programNameText.text);
        }

        #endregion

        public void SetHistoryValues(string programName, string goal, string date)
        {
            programNameText.text = String.IsNullOrEmpty(programName) ? "Default Program" : programName;
            goalText.text = goal;
            dateText.text = date;
        }
    }
}