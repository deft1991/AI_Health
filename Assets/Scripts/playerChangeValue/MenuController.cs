using System;
using System.Collections;
using System.Collections.Generic;
using data;
using global;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup nameScreen;
    [SerializeField] private CanvasGroup ageScreen;
    [SerializeField] private CanvasGroup genderScreen;
    [SerializeField] private CanvasGroup weightScreen;
    [SerializeField] private CanvasGroup heightScreen;
    [SerializeField] private CanvasGroup goalScreen;
    [SerializeField] private CanvasGroup recommendationScreen;
    [SerializeField] private CanvasGroup sorryScreen;
    [SerializeField] private CanvasGroup aboutScreen;
    [SerializeField] private CanvasGroup profileScreen;
    [SerializeField] private CanvasGroup workoutMuscleGroupScreen;
    [SerializeField] private CanvasGroup workoutDurationScreen;
    [SerializeField] private CanvasGroup workoutDifficultyLevel;
    [SerializeField] private CanvasGroup workoutRecommendation;


    private void Awake()
    {
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_DIFFICULTY_LEVEL, OpenWorkDifficultyLevel);
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_RECOMMENDATION, GenerateWorkoutRecommendation);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_DIFFICULTY_LEVEL, OpenWorkDifficultyLevel);
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_RECOMMENDATION, GenerateWorkoutRecommendation);
    }

    private void Start()
    {
        SetCurrentScreen(UiScreen.Sorry);
    }

    public void OpenSorry()
    {
        SetCurrentScreen(UiScreen.Sorry);
        Managers.BannerAdExample.Start();
    }

    public void OpenNutritionProgram()
    {
        PlayerIfoDto player = Managers.PlayerInfoManager.Player;

        if (player.name == null)
        {
            OpenName();
        }
        else if (player.age <= 0)
        {
            OpenAge();
        }
        else if (player.gender == GenderType.DEFAULT)
        {
            OpenGender();
        }
        else if (player.weight <= 0)
        {
            OpenWeight();
        }
        else if (player.height <= 0)
        {
            OpenHeight();
        }
        else if (player.goal == NutritionProgramGoal.DEFAULT)
        {
            OpenGoal();
        }
        else
        {
            GenerateNutritionProgramResult();
        }
    }


    public void OpenName()
    {
        SetCurrentScreen(UiScreen.Name);
    }

    public void OpenAge()
    {
        SetCurrentScreen(UiScreen.Age);
    }

    public void OpenGender()
    {
        SetCurrentScreen(UiScreen.Gender);
    }

    public void OpenWeight()
    {
        SetCurrentScreen(UiScreen.Weight);
    }

    public void OpenHeight()
    {
        SetCurrentScreen(UiScreen.Height);
    }

    public void OpenGoal()
    {
        SetCurrentScreen(UiScreen.Goal);
    }

    public void OpenAbout()
    {
        SetCurrentScreen(UiScreen.About);
    }

    public void OpenProfile()
    {
        SetCurrentScreen(UiScreen.Profile);
    }

    public void OpenTrainingProgram()
    {
        SetCurrentScreen(UiScreen.WorkoutMuscleGroupProgram);
    }

    public void OpenWorkDuration()
    {
        SetCurrentScreen(UiScreen.WorkoutDuration);
    }

    public void OpenWorkDifficultyLevel()
    {
        SetCurrentScreen(UiScreen.WorkoutDifficultyLevel);
    }

    public void GenerateNutritionProgramResult()
    {
        string recommendation = PlayerPrefs.GetString(PlayerInfoManager.NUTRITION_PROGRAM_RECOMMENDATION);
        if (recommendation != null)
        {
            SetCurrentScreen(UiScreen.NutritionProgramRecommendation);
            Messenger<string>.Broadcast(RecommendationEvent.NUTRITION_PROGRAM_RECEIVED, recommendation);
        }
        else
        {
            Managers.InterstitialAdExample.ShowAd();

            Managers.PlayerInfoManager.SavePlayer();
            SetCurrentScreen(UiScreen.NutritionProgramRecommendation);
            Managers.ChatGPTManager.GetNutritionProgramRecommendation();

            Managers.InterstitialAdExample.LoadAd();
        }
    }
    
    public void NutritionProgramTryAgain()
    {
        // todo open unity ads
        Managers.InterstitialAdExample.ShowAd();
        Managers.ChatGPTManager.GetNutritionProgramRecommendation();
        Managers.InterstitialAdExample.LoadAd();
    }

    public void GenerateWorkoutRecommendation()
    {
        // todo think about it
        Managers.InterstitialAdExample.ShowAd();

        SetCurrentScreen(UiScreen.WorkoutRecommendation);
        Managers.ChatGPTManager.GetWorkoutRecommendation();
        Managers.InterstitialAdExample.LoadAd();
    }

    public void WorkoutTryAgain()
    {
        // todo open unity ads
        Managers.InterstitialAdExample.ShowAd();
        Managers.ChatGPTManager.GetWorkoutRecommendation();
        Managers.InterstitialAdExample.LoadAd();
    }

    // public void SelectLevel(int level)
    // {
    //     basicSpawner.StartGame(GameMode.AutoHostOrClient);
    //     SceneManager.LoadScene("Level " + level);
    // }

    private void SetCurrentScreen(UiScreen screen)
    {
        Utility.SetCanvasGroupEnabled(nameScreen, screen == UiScreen.Name);
        Utility.SetCanvasGroupEnabled(ageScreen, screen == UiScreen.Age);
        Utility.SetCanvasGroupEnabled(genderScreen, screen == UiScreen.Gender);
        Utility.SetCanvasGroupEnabled(weightScreen, screen == UiScreen.Weight);
        Utility.SetCanvasGroupEnabled(heightScreen, screen == UiScreen.Height);
        Utility.SetCanvasGroupEnabled(goalScreen, screen == UiScreen.Goal);
        Utility.SetCanvasGroupEnabled(recommendationScreen, screen == UiScreen.NutritionProgramRecommendation);
        Utility.SetCanvasGroupEnabled(sorryScreen, screen == UiScreen.Sorry);
        Utility.SetCanvasGroupEnabled(aboutScreen, screen == UiScreen.About);
        Utility.SetCanvasGroupEnabled(profileScreen, screen == UiScreen.Profile);
        Utility.SetCanvasGroupEnabled(workoutMuscleGroupScreen, screen == UiScreen.WorkoutMuscleGroupProgram);
        Utility.SetCanvasGroupEnabled(workoutDurationScreen, screen == UiScreen.WorkoutDuration);
        Utility.SetCanvasGroupEnabled(workoutDifficultyLevel, screen == UiScreen.WorkoutDifficultyLevel);
        Utility.SetCanvasGroupEnabled(workoutRecommendation, screen == UiScreen.WorkoutRecommendation);
    }
}