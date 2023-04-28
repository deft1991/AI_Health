using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace data
{
    public class MealItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text value;
        [SerializeField] private Button deleteButton;

        private void Start()
        {
            deleteButton.onClick.AddListener(OnClickDelete);
        }

        public void SetText(string val)
        {
            value.text = val;
        }

        public void OnClickDelete()
        {
            var item = value.text;
            Debug.Log("MealItem OnClickDelete: " + item);
            Destroy(this.gameObject);
            Messenger<string>.Broadcast(MealEvent.DELETE_ITEM, item);
        }
    }
}