using System;
using data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class ProfileViewService : MonoBehaviour
    {
        [SerializeField] private TMP_InputField name;
        [SerializeField] private TMP_InputField age;
        [SerializeField] private TMP_InputField weight;
        [SerializeField] private TMP_InputField height;
        
        [SerializeField] private Dropdown gender;
        [SerializeField] private Dropdown goal;
        
        
        
        // todo add buttons

        private void Awake()
        {
            Messenger<string>.AddListener(ProfileChangeEvent.CHANGE_NAME, OnChangeName);
            Messenger<string>.AddListener(ProfileChangeEvent.CHANGE_AGE, OnChangeAge);
            Messenger<string>.AddListener(ProfileChangeEvent.CHANGE_WEIGHT, OnChangeWeight);
            Messenger<string>.AddListener(ProfileChangeEvent.CHANGE_HEIGHT, OnChangeHeight);
            Messenger<GenderType>.AddListener(ProfileChangeEvent.CHANGE_GENDER, OnChangeGender);
            Messenger<NutritionProgramGoal>.AddListener(ProfileChangeEvent.CHANGE_GOAL, OnChangeGoal);

            FillCurrentProfile();
        }
        
        private void OnDestroy()
        {
            Messenger<string>.RemoveListener(ProfileChangeEvent.CHANGE_NAME, OnChangeName);
            Messenger<string>.RemoveListener(ProfileChangeEvent.CHANGE_AGE, OnChangeAge);
            Messenger<string>.RemoveListener(ProfileChangeEvent.CHANGE_WEIGHT, OnChangeWeight);
            Messenger<string>.RemoveListener(ProfileChangeEvent.CHANGE_HEIGHT, OnChangeHeight);
            
            Messenger<GenderType>.RemoveListener(ProfileChangeEvent.CHANGE_GENDER, OnChangeGender);
            Messenger<NutritionProgramGoal>.RemoveListener(ProfileChangeEvent.CHANGE_GOAL, OnChangeGoal);
        }

        private void FillCurrentProfile()
        {
            PlayerIfoDto profile = Managers.PlayerInfoManager.Player;

            name.text = profile.name;
            age.text = profile.age.ToString();
            weight.text = profile.weight.ToString();
            height.text = profile.height.ToString();

            SetGender(profile.gender);
            SetGoal(profile.goal);
        }

        private void OnChangeGender(GenderType genderVal)
        {
            SetGender(genderVal);
        }
        
        private void OnChangeGoal(NutritionProgramGoal goalVal)
        {
            SetGoal(goalVal);
        }
        
        private void SetGender(GenderType genderVal)
        {
            switch (genderVal)
            {
                case GenderType.MALE:
                    gender.value = 0;
                    break;
                case GenderType.FEMALE:
                    gender.value = 1;
                    break;
            }
        }

        private void SetGoal(NutritionProgramGoal profileGoal)
        {
            switch (profileGoal)
            {
                case NutritionProgramGoal.DRY:
                    goal.value = 0;
                    break;
                case NutritionProgramGoal.SAME:
                    goal.value = 1;
                    break;
                case NutritionProgramGoal.INCREASE:
                    goal.value = 2;
                    break;
            }
        }

        private void OnChangeName(string val)
        {
            name.text = val;
        }

        private void OnChangeAge(string val)
        {
            age.text = val;
        }

        private void OnChangeWeight(string val)
        {
            weight.text = val;
        }


        private void OnChangeHeight(string val)
        {
            height.text = val;
        }
    }
}