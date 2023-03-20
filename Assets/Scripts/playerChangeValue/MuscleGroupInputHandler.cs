using System;
using System.Collections.Generic;
using data;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class MuscleGroupInputHandler : MonoBehaviour
    {
        [SerializeField] private MuscleGroupToggle[] muscleGroupToggles;

        private HashSet<MuscleGroupType> programs;


        #region PUBLIC

        public void OnClickNext()
        {
            Managers.PlayerInfoManager.Programs = programs;
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_WORK_DURATION);
        }

        #endregion

        #region PRIVATE

        private void Start()
        {
            foreach (MuscleGroupToggle muscleGroupToggle in muscleGroupToggles)
            {
                var component = muscleGroupToggle.GetComponent<Toggle>();
                component.onValueChanged.AddListener(delegate { ShoulderValueChanged(muscleGroupToggle); });
            }

            programs = new HashSet<MuscleGroupType>();
        }

        private void ShoulderValueChanged(MuscleGroupToggle muscleGroup)
        {
            Toggle toggle = muscleGroup.GetComponent<Toggle>();
            if (toggle.isOn)
            {
                programs.Add(muscleGroup.Type);
            }
            else
            {
                programs.Remove(muscleGroup.Type);
            }
        }

        #endregion
    }
}