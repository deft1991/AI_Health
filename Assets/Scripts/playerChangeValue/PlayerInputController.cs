using System;
using global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class PlayerInputController : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField ageInput;
        [SerializeField] private TMP_InputField weightInput;
        [SerializeField] private TMP_InputField heightInput;

        [SerializeField] private Button ageNextButton;
        [SerializeField] private Button genderNextButton;
        [SerializeField] private Button heightNextButton;
        [SerializeField] private Button goalNextButton;

        void Start()
        {
            //Adds a listener to the main input field and invokes a method when the value changes.
            nameInput.onValueChanged.AddListener(NameChangeCheck);
            ageInput.onValueChanged.AddListener(AgeChangeCheck);
            weightInput.onValueChanged.AddListener(WeightChangeCheck);
            heightInput.onValueChanged.AddListener(HeightChangeCheck);
        }

        // Update is called once per frame
        void Update()
        {
        }

        // Invoked when the value of the text field changes.
        public void NameChangeCheck(string val)
        {
            Debug.Log("Name Changed: " + val);
            Managers.PlayerInfoManager.Player.name = val;
            ageNextButton.gameObject.SetActive(!String.IsNullOrEmpty(val));
            Messenger<string>.Broadcast(ProfileChangeEvent.CHANGE_NAME, val);
        }

        public void AgeChangeCheck(string val)
        {
            Debug.Log("Age Changed: " + val);

            if (String.IsNullOrEmpty(val))
            {
                genderNextButton.gameObject.SetActive(false);
            }
            else
            {
                Managers.PlayerInfoManager.Player.age = Convert.ToInt32(val);
                genderNextButton.gameObject.SetActive(true);
                Messenger<string>.Broadcast(ProfileChangeEvent.CHANGE_AGE, val);
            }
        }

        public void WeightChangeCheck(string val)
        {
            Debug.Log("Weight Changed: " + val);
            if (String.IsNullOrEmpty(val))
            {
                heightNextButton.gameObject.SetActive(false);
            }
            else
            {
                Managers.PlayerInfoManager.Player.weight = Convert.ToInt32(val);
                heightNextButton.gameObject.SetActive(true);
                Messenger<string>.Broadcast(ProfileChangeEvent.CHANGE_WEIGHT, val);
            }
        }

        public void HeightChangeCheck(string val)
        {
            Debug.Log("Height Changed: " + val);
            if (String.IsNullOrEmpty(val))
            {
                goalNextButton.gameObject.SetActive(false);
            }
            else
            {
                Managers.PlayerInfoManager.Player.height = Convert.ToInt32(val);
                goalNextButton.gameObject.SetActive(true);
                Messenger<string>.Broadcast(ProfileChangeEvent.CHANGE_HEIGHT, val);
            }
        }
    }
}