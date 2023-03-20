using System;
using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;

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
    [SerializeField] private CanvasGroup muscleGroupScreen;
    [SerializeField] private CanvasGroup workoutDurationScreen;


    private void Awake()
    {
        Messenger.AddListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(ScreenChangeEvent.GO_TO_WORK_DURATION, OpenWorkDuration);
    }

    private void Start()
    {
        SetCurrentScreen(UiScreen.Sorry);
    }

    public void OpenSorry()
    {
        SetCurrentScreen(UiScreen.Sorry);
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
            GenerateResult();
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
        SetCurrentScreen(UiScreen.MuscleGroupProgram);
    }

    public void OpenWorkDuration()
    {
        SetCurrentScreen(UiScreen.WorkoutDuration);
    }

    public void GenerateResult()
    {
        string recommendation = PlayerPrefs.GetString("recommendation");
        if (recommendation != null)
        {
            SetCurrentScreen(UiScreen.Recommendation);
            Messenger<string>.Broadcast(RecommendationEvent.RECEIVED, recommendation);
        }
        else
        {
            Managers.InterstitialAdExample.ShowAd();

            Managers.PlayerInfoManager.SavePlayer();
            SetCurrentScreen(UiScreen.Recommendation);
            Managers.ChatGPTManager.GetRecommendation();

            Managers.InterstitialAdExample.LoadAd();
        }
    }

    public void TryAgain()
    {
        // todo open unity ads
        Managers.InterstitialAdExample.ShowAd();
        Managers.ChatGPTManager.GetRecommendation();
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
        Utility.SetCanvasGroupEnabled(recommendationScreen, screen == UiScreen.Recommendation);
        Utility.SetCanvasGroupEnabled(sorryScreen, screen == UiScreen.Sorry);
        Utility.SetCanvasGroupEnabled(aboutScreen, screen == UiScreen.About);
        Utility.SetCanvasGroupEnabled(profileScreen, screen == UiScreen.Profile);
        Utility.SetCanvasGroupEnabled(muscleGroupScreen, screen == UiScreen.MuscleGroupProgram);
        Utility.SetCanvasGroupEnabled(workoutDurationScreen, screen == UiScreen.WorkoutDuration);
    }
}