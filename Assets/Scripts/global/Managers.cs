using System.Collections;
using System.Collections.Generic;
using advert;
using global.inapp;
using global.manager;
using nutritionProgram.service;
using pushNotification.manager;
using UnityEngine;


/* 
 * Required components
 */
namespace global
{
    [RequireComponent(typeof(FillDataManager))]
    [RequireComponent(typeof(ChatGptManager))]
    [RequireComponent(typeof(PlayerInfoManager))]
    [RequireComponent(typeof(AdsInitializerManager))]
    [RequireComponent(typeof(BannerAdExample))]
    [RequireComponent(typeof(InterstitialAdExample))]
    [RequireComponent(typeof(RewardedAdsButton))]
    [RequireComponent(typeof(MealInfoManager))]
    [RequireComponent(typeof(StuckManager))]
    [RequireComponent(typeof(ShowAdManager))]
    [RequireComponent(typeof(MyIAPManager))]
    [RequireComponent(typeof(NutritionProgramHistoryManager))]
    [RequireComponent(typeof(PushNotificationManager))]
    [RequireComponent(typeof(GoogleReviewManager))]
    public class Managers : MonoBehaviour
    {
    
        public static FillDataManager FillDataManager { get; private set; }
        public static ChatGptManager ChatGPTManager { get; private set; }
        public static PlayerInfoManager PlayerInfoManager { get; private set; }
        public static AdsInitializerManager AdsInitializerManager { get; private set; }
        public static BannerAdExample BannerAdExample { get; private set; }
        public static InterstitialAdExample InterstitialAdExample { get; private set; }
        public static RewardedAdsButton RewardedAdsButton { get; private set; }
        public static MealInfoManager MealInfoManager { get; private set; }
        public static StuckManager StuckManager { get; private set; }
        public static ShowAdManager ShowAdManager { get; private set; }
        public static MyIAPManager MyIAPManager { get; private set; }
        public static NutritionProgramHistoryManager NutritionProgramHistoryManager { get; private set; }
        public static PushNotificationManager PushNotificationManager { get; private set; }
        public static GoogleReviewManager GoogleReviewManager { get; private set; }

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
            ChatGPTManager = GetComponent<ChatGptManager>();
            PlayerInfoManager = GetComponent<PlayerInfoManager>();
            AdsInitializerManager = GetComponent<AdsInitializerManager>();
            BannerAdExample = GetComponent<BannerAdExample>();
            InterstitialAdExample = GetComponent<InterstitialAdExample>();
            RewardedAdsButton = GetComponent<RewardedAdsButton>();
            MealInfoManager = GetComponent<MealInfoManager>();
            StuckManager = GetComponent<StuckManager>();
            ShowAdManager = GetComponent<ShowAdManager>();
            MyIAPManager = GetComponent<MyIAPManager>();
            NutritionProgramHistoryManager = GetComponent<NutritionProgramHistoryManager>();
            PushNotificationManager = GetComponent<PushNotificationManager>();
            GoogleReviewManager = GetComponent<GoogleReviewManager>();

            _startSequence = new List<IGameManager>();
            _startSequence.Add(AdsInitializerManager);
            _startSequence.Add(FillDataManager);
            _startSequence.Add(ChatGPTManager);
            _startSequence.Add(PlayerInfoManager);
            _startSequence.Add(MealInfoManager);
            _startSequence.Add(StuckManager);
            _startSequence.Add(ShowAdManager);
            _startSequence.Add(MyIAPManager);
            _startSequence.Add(NutritionProgramHistoryManager);
            _startSequence.Add(PushNotificationManager);
            _startSequence.Add(GoogleReviewManager);
            
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
}