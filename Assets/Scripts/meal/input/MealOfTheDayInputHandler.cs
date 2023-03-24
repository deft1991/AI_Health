using System;
using System.Collections.Generic;
using data;
using global;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class MealOfTheDayInputHandler : MonoBehaviour
    {
        [SerializeField] private Button breakfastButton;
        [SerializeField] private Button lunchButton;
        [SerializeField] private Button dinnerButton;
        [SerializeField] private Button nextButton;
        private MealOfTheDayType _mealOfTheDayType;


        #region PUBLIC

        public void OnClickNext()
        {
            Managers.MealInfoManager.Meal.MealOfTheDayType = _mealOfTheDayType;
            
            Messenger.Broadcast(MealEvent.RESET_GRID_LAYOUT_GROUP);
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_MEAL_ITEMS);
        }

        #endregion

        #region PRIVATE

        private void Start()
        {
            breakfastButton.onClick.AddListener(delegate { OnMealOfTheDay(MealOfTheDayType.BREAKFAST); });
            lunchButton.onClick.AddListener(delegate { OnMealOfTheDay(MealOfTheDayType.LUNCH); });
            dinnerButton.onClick.AddListener(delegate { OnMealOfTheDay(MealOfTheDayType.DINNER); });
        }
        
        private void OnMealOfTheDay(MealOfTheDayType mealOfTheDay)
        {
            Debug.Log("Meal of the day: " + mealOfTheDay);
            _mealOfTheDayType = mealOfTheDay;
            nextButton.gameObject.SetActive(true);
        }

        #endregion
    }
}