using System;
using global.manager;
using nutritionProgram.history.@event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace nutritionProgram.history.controller
{
    public class SaveNutritionProgramController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField programNameInput;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button cancelButton;

        private string _programName;

        #region PUBLIC

        public void OnClickSave()
        {
            string recommendation = PlayerPrefs.GetString(PlayerInfoManager.NUTRITION_PROGRAM_RECOMMENDATION);
            Messenger<string, string>.Broadcast(NutritionProgramHistoryEvent.SAVE_HISTORY, _programName,
                recommendation);
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_NUTRITION_PROGRAM_HISTORY);
        }

        public void OnClickCancel()
        {
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_NUTRITION_PROGRAM_RECOMMENDATION);
        }

        #endregion

        #region PRIVATE

        private void Start()
        {
            programNameInput.onValueChanged.AddListener(ProgramNameChange);
        }

        // Invoked when the value of the text field changes.
        public void ProgramNameChange(string val)
        {
            Debug.Log("Program Name Changed: " + val);
            _programName = val;
            saveButton.gameObject.SetActive(!String.IsNullOrEmpty(_programName));
        }

        #endregion
    }
}