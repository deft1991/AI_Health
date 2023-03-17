using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        SetCurrentScreen(UiScreen.Sorry);
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
        Managers.InterstitialAdExample.ShowAd();
        SetCurrentScreen(UiScreen.Weight);
        Managers.InterstitialAdExample.LoadAd();
    }

    public void OpenHeight()
    {
        SetCurrentScreen(UiScreen.Height);
    }

    public void OpenGoal()
    {
        SetCurrentScreen(UiScreen.Goal);
    }

    public void GenerateResult()
    {
        // todo open unity ads
        Managers.InterstitialAdExample.ShowAd();
        
        Managers.PlayerInfoManager.SavePlayer();
        SetCurrentScreen(UiScreen.Recommendation);
        Managers.ChatGPTManager.GetRecommendation();
        
        Managers.InterstitialAdExample.LoadAd();
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
    }
}