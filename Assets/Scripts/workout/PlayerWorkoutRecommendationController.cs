using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWorkoutRecommendationController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultField;
    [SerializeField] private Button tryAgain;

    private void Awake()
    {
        Messenger<string>.AddListener(RecommendationEvent.WORKOUT_PROGRAM_RECEIVED, OnReceiveRecommendation);
        Messenger.AddListener(RecommendationEvent.WORKOUT_PROGRAM_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(RecommendationEvent.WORKOUT_PROGRAM_RECEIVED, OnReceiveRecommendation);
        Messenger.RemoveListener(RecommendationEvent.WORKOUT_PROGRAM_IN_PROCESS, OnInProcessRecommendation);
    }

    private void OnReceiveRecommendation(string recommendation)
    {
        resultField.text = recommendation;
        tryAgain.gameObject.SetActive(true);
    }

    private void OnInProcessRecommendation()
    {
        resultField.text = "Your workout program is preparing...";
        tryAgain.gameObject.SetActive(false);
    }
}