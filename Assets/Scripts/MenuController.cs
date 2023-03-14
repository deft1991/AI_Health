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

    private void Start()
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

    public void GenerateResult()
    {
        // todo open unity ads
        Managers.ChatGPTManager.GetRecommendation();
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
    }
}