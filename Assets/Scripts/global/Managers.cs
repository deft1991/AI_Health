using System.Collections;
using System.Collections.Generic;
using global;
using playerChangeValue;
using UnityEngine;


/* 
 * Required components
 */
[RequireComponent(typeof(FillDataManager))]
[RequireComponent(typeof(ChatGPTManager))]
[RequireComponent(typeof(PlayerInfoManager))]
[RequireComponent(typeof(AdsInitializer))]
[RequireComponent(typeof(BannerAdExample))]
[RequireComponent(typeof(InterstitialAdExample))]
[RequireComponent(typeof(RewardedAdsButton))]
public class Managers : MonoBehaviour
{
    
    public static FillDataManager FillDataManager { get; private set; }
    public static ChatGPTManager ChatGPTManager { get; private set; }
    public static PlayerInfoManager PlayerInfoManager { get; private set; }
    public static AdsInitializer AdsInitializer { get; private set; }
    public static BannerAdExample BannerAdExample { get; private set; }
    public static InterstitialAdExample InterstitialAdExample { get; private set; }
    public static RewardedAdsButton RewardedAdsButton { get; private set; }

    /*
     * List of all IGameManagers
     */
    private List<IGameManager> _startSequence;

    /**
     * Executes before start
     * uses for initialization before all other modules
     */
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        FillDataManager = GetComponent<FillDataManager>();
        ChatGPTManager = GetComponent<ChatGPTManager>();
        PlayerInfoManager = GetComponent<PlayerInfoManager>();
        AdsInitializer = GetComponent<AdsInitializer>();
        BannerAdExample = GetComponent<BannerAdExample>();
        InterstitialAdExample = GetComponent<InterstitialAdExample>();
        RewardedAdsButton = GetComponent<RewardedAdsButton>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(AdsInitializer);
        _startSequence.Add(FillDataManager);
        _startSequence.Add(ChatGPTManager);
        _startSequence.Add(PlayerInfoManager);
        // _startSequence.Add(BannerAdExample);
        // _startSequence.Add(InterstitialAdExample);
        // _startSequence.Add(RewardedAdsButton);
        /*
         * MUST be last manager in loading
         */
        // _startSequence.Add(Data);

        StartCoroutine(StartupManagers());
    }
    
    private IEnumerator StartupManagers()
    {
        // NetworkService networkService = new NetworkService();
        foreach (IGameManager gameManager in _startSequence)
        {
            // gameManager.Startup(networkService);
            gameManager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        /*
         * Do it again and again while initializing all modules
         */
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager gameManager in _startSequence)
            {
                if (ManagerStatus.Started == gameManager.Status)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }

            /*
             * Stop on one tick before the next check
             */
            yield return null;
        }
        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
        
        BannerAdExample.Startup();
        InterstitialAdExample.Startup();
        // RewardedAdsButton.Startup();
    }
}