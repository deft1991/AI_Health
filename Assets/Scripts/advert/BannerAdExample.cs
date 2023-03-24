using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace advert
{
    public class BannerAdExample : MonoBehaviour, IGameManager
    {
        // For the purpose of this example, these buttons are for functionality testing:
        // [SerializeField] Button _loadBannerButton;
        // [SerializeField] Button _showBannerButton;
        // [SerializeField] Button _hideBannerButton;

        [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

        [SerializeField] string _androidAdUnitId = "Banner_Android";
        [SerializeField] string _iOsAdUnitId = "Banner_iOS";
        public ManagerStatus Status { get; private set; }
    
        private static string _adUnitId;

        public void Start()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
            // Disable the button until an ad is ready to show:
            // _showBannerButton.interactable = false;
            // _hideBannerButton.interactable = false;

            // Set the banner position:
            Advertisement.Banner.SetPosition(_bannerPosition);

            // Configure the Load Banner button to call the LoadBanner() method when clicked:
            // _loadBannerButton.onClick.AddListener(LoadBanner);
            // _loadBannerButton.interactable = true;
            LoadBanner();
            ShowBannerAd();
        }
    
        public void Startup()
        {
            Debug.Log("BannerAdExample starting...");


            Status = ManagerStatus.Initializing;
        
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
            // Set the banner position:
            Advertisement.Banner.SetPosition(_bannerPosition);
            LoadBanner();
            ShowBannerAd();

            Status = ManagerStatus.Started;
            Debug.Log("BannerAdExample: started");
        }

    

        // Implement a method to call when the Load Banner button is clicked:
        public void LoadBanner()
        {
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(_adUnitId, options);
        }

        // Implement code to execute when the loadCallback event triggers:
        void OnBannerLoaded()
        {
            Debug.Log("Banner loaded");

            // Configure the Show Banner button to call the ShowBannerAd() method when clicked:
            // _showBannerButton.onClick.AddListener(ShowBannerAd);
            // Configure the Hide Banner button to call the HideBannerAd() method when clicked:
            // _hideBannerButton.onClick.AddListener(HideBannerAd);

            // Enable both buttons:
            // _showBannerButton.interactable = true;
            // _hideBannerButton.interactable = true;
        }

        // Implement code to execute when the load errorCallback event triggers:
        void OnBannerError(string message)
        {
            Debug.Log($"Banner Error: {message}");
            // Optionally execute additional code, such as attempting to load another ad.
        }

        // Implement a method to call when the Show Banner button is clicked:
        public void ShowBannerAd()
        {
            // Set up options to notify the SDK of show events:
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
 
            // Show the loaded Banner Ad Unit:
            Advertisement.Banner.Show(_adUnitId, options);
        }

        private IEnumerator RepeatShowBanner()
        {
            yield return new WaitForSeconds(1);
            ShowBannerAd();
        }

        // Implement a method to call when the Hide Banner button is clicked:
        void HideBannerAd()
        {
            // Hide the banner:
            Advertisement.Banner.Hide();
        }

        void OnBannerClicked()
        {
        }

        void OnBannerShown()
        {
        }

        void OnBannerHidden()
        {
        }

        void OnDestroy()
        {
            // Clean up the listeners:
            // _loadBannerButton.onClick.RemoveAllListeners();
            // _showBannerButton.onClick.RemoveAllListeners();
            // _hideBannerButton.onClick.RemoveAllListeners();
        }
    }
}