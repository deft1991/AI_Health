using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IGameManager
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;

    public ManagerStatus Status { get; private set; }
    
    void Awake()
    {
        Debug.Log("AdsInitializer Awake. Advertisement.isSupported = " + Advertisement.isSupported);
        if (Advertisement.isSupported)
        {
            InitializeAds();
        }
    }
    
    public void Startup()
    {
        Debug.Log("AdsInitializer starting...");
        Status = ManagerStatus.Initializing;
        
        Status = ManagerStatus.Started;
        Debug.Log("AdsInitializer: started");
    }


    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
        Debug.Log("AdsInitializer InitializeAds. _gameId = " + _gameId);
        Advertisement.Initialize(_gameId, _testMode, this);
        Debug.Log("AdsInitializer InitializeAds complete.");
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}