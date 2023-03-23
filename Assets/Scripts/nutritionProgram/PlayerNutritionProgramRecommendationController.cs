using System;
using global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNutritionProgramRecommendationController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultField;
    [SerializeField] private Button tryAgain;

    private void Awake()
    {
        Messenger<string>.AddListener(RecommendationEvent.NUTRITION_PROGRAM_RECEIVED, OnReceiveRecommendation);
        Messenger.AddListener(RecommendationEvent.NUTRITION_PROGRAM_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnDestroy()
    {
        Messenger<string>.AddListener(RecommendationEvent.NUTRITION_PROGRAM_RECEIVED, OnReceiveRecommendation);
        Messenger.AddListener(RecommendationEvent.NUTRITION_PROGRAM_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnReceiveRecommendation(string recommendation)
    {
        PlayerPrefs.SetString(PlayerInfoManager.NUTRITION_PROGRAM_RECOMMENDATION, recommendation);
        resultField.text = recommendation;
        tryAgain.gameObject.SetActive(true);
    }

    private void OnInProcessRecommendation()
    {
        resultField.text = "Your nutrition program is preparing...";
        tryAgain.gameObject.SetActive(false);
    }
}