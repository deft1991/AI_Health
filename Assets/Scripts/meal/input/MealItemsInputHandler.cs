using System;
using System.Collections.Generic;
using data;
using playerChangeValue.util;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace meal.input
{
    public class MealItemsInputHandler : MonoBehaviour
    {
        [SerializeField] private GameObject menuItemPrefab;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private TMP_InputField mealItemInput;

        [SerializeField] private Button nextButton;

        private HashSet<string> _mealItems;

        private void Awake()
        {
            Messenger.AddListener(MealEvent.RESET_GRID_LAYOUT_GROUP, OnResetGridLayoutGroup);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(MealEvent.RESET_GRID_LAYOUT_GROUP, OnResetGridLayoutGroup);
        }

        private void OnResetGridLayoutGroup()
        {
            GridLayoutHelper.ClearGridLayoutGroup(gridLayout);
            nextButton.gameObject.SetActive(false);
            _mealItems.Clear();
        }

        private void Start()
        {
            _mealItems = new HashSet<string>();

            GridLayoutHelper.ClearGridLayoutGroup(gridLayout);
            //Adds a listener to the main input field and invokes a method when the value changes.
            mealItemInput.onValueChanged.AddListener(ValueChange);
        }

        #region PUBLIC

        public void OnClickNext()
        {
            Managers.MealInfoManager.Meal.mealItems = _mealItems;
            Messenger.Broadcast(ScreenChangeEvent.GO_TO_MEAL_RECOMMENDATION);
        }

        public void ValueChange(string val)
        {
            Debug.Log("Meal Item Changed: " + val);
        }

        public void OnClickAddItem()
        {
            var mealItemValue = mealItemInput.text;
            if (!string.IsNullOrEmpty(mealItemValue))
            {
                nextButton.gameObject.SetActive(true);
                if (!_mealItems.Contains(mealItemValue))
                {
                    _mealItems.Add(mealItemValue);
                    //
                    GameObject newButton = Instantiate(menuItemPrefab, gridLayout.transform, false);
                    MealItem mealItem = newButton.GetComponent<MealItem>();
                    mealItem.SetText(mealItemValue);
                }
            }
            else
            {
                nextButton.gameObject.SetActive(false);
            }
        }

        public void OnClickRemoveItem()
        {
            // todo add remove items
        }

        #endregion
    }
}