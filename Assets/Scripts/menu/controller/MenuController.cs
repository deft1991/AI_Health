using System;
using System.Collections;
using System.Collections.Generic;
using advert.@event;
using data;
using global;
using global.manager;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [Header("Nutrition Program")] [SerializeField]
    private CanvasGroup nameScreen;

    [SerializeField] private CanvasGroup ageScreen;
    [SerializeField] private CanvasGroup genderScreen;
    [SerializeField] private CanvasGroup weightScreen;
    [SerializeField] private CanvasGroup heightScreen;
    [SerializeField] private CanvasGroup goalScreen;
    [SerializeField] private CanvasGroup recommendationScreen;

    [Header("Other")] [SerializeField] private CanvasGroup sorryScreen;
    [SerializeField] private CanvasGroup aboutScreen;

    [Header("Profile Info")] [SerializeField]
    private CanvasGroup profileScreen;

    [Header("Workout")] [SerializeField] private CanvasGroup workoutMuscleGroupScreen;
    [SerializeField] private CanvasGroup workoutDurationScreen;
    [SerializeField] private CanvasGroup workoutDifficultyLevelScreen;
    [SerializeField] private CanvasGroup workoutRecommendationScreen;

    [Header("Meal")] [SerializeField] private CanvasGroup mealOfTheDayScreen;
    [SerializeField] private CanvasGroup mealItemsScreen;
    [SerializeField] private CanvasGroup MealRecommendationScreen;


    private void Awake()
    {
        // workout
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_DIFFICULTY_LEVEL, OpenWorkDifficultyLevel);
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_RECOMMENDATION, GenerateWorkoutRecommendation);
        // meal
        Messenger.AddListener(ScreenChangeEvent.GO_TO_MEAL_ITEMS, OpenMealItems);
        Messenger.AddListener(ScreenChangeEvent.GO_TO_MEAL_RECOMMENDATION, OpenMealRecommendation);
    }

    private void OnDestroy()
    {
        // workout
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_DIFFICULTY_LEVEL, OpenWorkDifficultyLevel);
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_WORKOUT_RECOMMENDATION, GenerateWorkoutRecommendation);
        // meal
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_MEAL_ITEMS, OpenMealItems);
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_MEAL_RECOMMENDATION, OpenMealRecommendation);
    }

    private void Start()
    {
        SetCurrentScreen(UiScreenType.Sorry);
    }

    public void OpenSorry()
    {
        SetCurrentScreen(UiScreenType.Sorry);
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
        SetCurrentScreen(UiScreenType.Name);
    }

    public void OpenAge()
    {
        SetCurrentScreen(UiScreenType.Age);
    }

    public void OpenGender()
    {
        SetCurrentScreen(UiScreenType.Gender);
    }

    public void OpenWeight()
    {
        SetCurrentScreen(UiScreenType.Weight);
    }

    public void OpenHeight()
    {
        SetCurrentScreen(UiScreenType.Height);
    }

    public void OpenGoal()
    {
        SetCurrentScreen(UiScreenType.Goal);
    }

    public void OpenAbout()
    {
        SetCurrentScreen(UiScreenType.About);
    }

    public void OpenProfile()
    {
        SetCurrentScreen(UiScreenType.Profile);
    }

    public void OpenTrainingProgram()
    {
        SetCurrentScreen(UiScreenType.WorkoutMuscleGroupProgram);
    }

    public void OpenWorkDuration()
    {
        SetCurrentScreen(UiScreenType.WorkoutDuration);
    }

    public void OpenWorkDifficultyLevel()
    {
        SetCurrentScreen(UiScreenType.WorkoutDifficultyLevel);
    }

    public void OpenMealOfTheDay()
    {
        SetCurrentScreen(UiScreenType.MealOfTheDay);
    }

    public void OpenMealItems()
    {
        SetCurrentScreen(UiScreenType.MealItems);
    }

    public void OpenMealRecommendation()
    {
        GenerateMealRecommendationResult();
    }

    public void GenerateNutritionProgramResult()
    {
        string recommendation = PlayerPrefs.GetString(PlayerInfoManager.NUTRITION_PROGRAM_RECOMMENDATION);
        if (!string.IsNullOrEmpty(recommendation))
        {
            SetCurrentScreen(UiScreenType.NutritionProgramRecommendation);
            Messenger<string>.Broadcast(RecommendationEvent.NUTRITION_PROGRAM_RECEIVED, recommendation);
        }
        else
        {
            Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
            Managers.PlayerInfoManager.SavePlayer();
            SetCurrentScreen(UiScreenType.NutritionProgramRecommendation);
            Managers.ChatGPTManager.GetNutritionProgramRecommendation();
        }
    }

    public void NutritionProgramTryAgain()
    {
        Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
        Managers.ChatGPTManager.GetNutritionProgramRecommendation();
    }

    public void GenerateWorkoutRecommendation()
    {
        Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
        SetCurrentScreen(UiScreenType.WorkoutRecommendation);
        Managers.ChatGPTManager.GetWorkoutRecommendation();
    }

    public void WorkoutTryAgain()
    {
        Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
        Managers.ChatGPTManager.GetWorkoutRecommendation();
    }

    public void GenerateMealRecommendationResult()
    {
        Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
        SetCurrentScreen(UiScreenType.MealRecommendation);
        Managers.ChatGPTManager.GetMealRecommendation();
    }

    public void MealRecommendationTryAgain()
    {
        Messenger.Broadcast(AdvEvent.SHOW_INTERSTITIAL);
        Managers.ChatGPTManager.GetMealRecommendation();
    }


    // public void SelectLevel(int level)
    // {
    //     basicSpawner.StartGame(GameMode.AutoHostOrClient);
    //     SceneManager.LoadScene("Level " + level);
    // }

    private void SetCurrentScreen(UiScreenType screenType)
    {
        Utility.SetCanvasGroupEnabled(nameScreen, screenType == UiScreenType.Name);
        Utility.SetCanvasGroupEnabled(ageScreen, screenType == UiScreenType.Age);
        Utility.SetCanvasGroupEnabled(genderScreen, screenType == UiScreenType.Gender);
        Utility.SetCanvasGroupEnabled(weightScreen, screenType == UiScreenType.Weight);
        Utility.SetCanvasGroupEnabled(heightScreen, screenType == UiScreenType.Height);
        Utility.SetCanvasGroupEnabled(goalScreen, screenType == UiScreenType.Goal);
        Utility.SetCanvasGroupEnabled(recommendationScreen, screenType == UiScreenType.NutritionProgramRecommendation);
        Utility.SetCanvasGroupEnabled(sorryScreen, screenType == UiScreenType.Sorry);
        Utility.SetCanvasGroupEnabled(aboutScreen, screenType == UiScreenType.About);
        Utility.SetCanvasGroupEnabled(profileScreen, screenType == UiScreenType.Profile);
        Utility.SetCanvasGroupEnabled(workoutMuscleGroupScreen, screenType == UiScreenType.WorkoutMuscleGroupProgram);
        Utility.SetCanvasGroupEnabled(workoutDurationScreen, screenType == UiScreenType.WorkoutDuration);
        Utility.SetCanvasGroupEnabled(workoutDifficultyLevelScreen, screenType == UiScreenType.WorkoutDifficultyLevel);
        Utility.SetCanvasGroupEnabled(workoutRecommendationScreen, screenType == UiScreenType.WorkoutRecommendation);

        Utility.SetCanvasGroupEnabled(mealOfTheDayScreen, screenType == UiScreenType.MealOfTheDay);
        Utility.SetCanvasGroupEnabled(mealItemsScreen, screenType == UiScreenType.MealItems);
        Utility.SetCanvasGroupEnabled(MealRecommendationScreen, screenType == UiScreenType.MealRecommendation);
    }
}