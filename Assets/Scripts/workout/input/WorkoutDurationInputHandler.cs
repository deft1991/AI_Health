using System;
using System.Collections.Generic;
using data;
using global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class WorkoutDurationInputHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField durationInput;
        [SerializeField] private Button nextButton;

        private int workoutDuration;


        #region PUBLIC

        public void OnClickNext()
        {
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_WORK_WORKOUT_DIFFICULTY_LEVEL);
        }

        #endregion

        #region PRIVATE

        private void Start()
        {
            durationInput.onValueChanged.AddListener(DurationChangeCheck);
        }

        private void DurationChangeCheck(string val)
        {
            if (String.IsNullOrEmpty(val))
            {
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                var duration = Convert.ToInt32(val);
                Debug.Log("Workout Duration Changed: " + duration);
                Managers.PlayerInfoManager.WorkoutDuration = duration;

                nextButton.gameObject.SetActive(duration > 0);
            }
        }

        #endregion
    }
}