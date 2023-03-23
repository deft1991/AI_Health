using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MealRecommendationController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultField;
    [SerializeField] private Button tryAgain;

    private void Awake()
    {
        Messenger<string>.AddListener(RecommendationEvent.MEAL_RECOMMENDATION_RECEIVED, OnReceiveRecommendation);
        Messenger.AddListener(RecommendationEvent.MEAL_RECOMMENDATION_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(RecommendationEvent.MEAL_RECOMMENDATION_RECEIVED, OnReceiveRecommendation);
        Messenger.RemoveListener(RecommendationEvent.MEAL_RECOMMENDATION_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnReceiveRecommendation(string recommendation)
    {
        resultField.text = recommendation;
        tryAgain.gameObject.SetActive(true);
    }

    private void OnInProcessRecommendation()
    {
        resultField.text = "Your meal recommendation is preparing...";
        tryAgain.gameObject.SetActive(false);
    }
}