using System;
using System.Collections.Generic;
using data;
using global;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class WorkoutDifficultyLevelInputHandler : MonoBehaviour
    {
        [SerializeField] private Button beginnerButton;
        [SerializeField] private Button intermediateButton;
        [SerializeField] private Button professionalButton;
        [SerializeField] private Button nextButton;
        private WorkoutDifficultyLevelType _workoutDifficultyLevelType;


        #region PUBLIC

        public void OnClickNext()
        {
            Managers.PlayerInfoManager.WorkoutDifficultyLevel = _workoutDifficultyLevelType;
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_WORK_WORKOUT_RECOMMENDATION);
        }

        #endregion

        #region PRIVATE

        private void Start()
        {
            beginnerButton.onClick.AddListener(delegate { OnClickLevel(WorkoutDifficultyLevelType.BEGINNER); });
            intermediateButton.onClick.AddListener(delegate { OnClickLevel(WorkoutDifficultyLevelType.INTERMEDIATE); });
            professionalButton.onClick.AddListener(delegate { OnClickLevel(WorkoutDifficultyLevelType.PROFESSIONAL); });
        }
        
        private void OnClickLevel(WorkoutDifficultyLevelType levelType)
        {
            Debug.Log("Workout difficulty level: " + levelType);
            _workoutDifficultyLevelType = levelType;
            nextButton.gameObject.SetActive(true);
        }

        #endregion
    }
}