using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class PlayerRecommendationController : MonoBehaviour
    {

        [SerializeField] private TMP_Text resultField;
        [SerializeField] private Button tryAgain;
        
        private void Awake()
        {
            Messenger<string>.AddListener(RecommendationEvent.RECEIVED, OnReceiveRecommendation);
            Messenger.AddListener(RecommendationEvent.IN_PROCESS, OnInProcessRecommendation);
        }

        private void OnDestroy()
        {
            Messenger<string>.AddListener(RecommendationEvent.RECEIVED, OnReceiveRecommendation);
            Messenger.AddListener(RecommendationEvent.IN_PROCESS, OnInProcessRecommendation);
        }
        
        private void OnReceiveRecommendation(string recommendation)
        {
            resultField.text = recommendation;
            tryAgain.gameObject.SetActive(true);
        }       
        
        private void OnInProcessRecommendation()
        {
            resultField.text = "Your nutrition program is preparing...";
            tryAgain.gameObject.SetActive(false);
        }
    }
}