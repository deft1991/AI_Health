using System;
using advert.@event;
using global.inapp;
using UnityEngine;

namespace global.manager
{
    public class ShowAdManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public void Startup()
        {
            Debug.Log("StuckManager starting...");

            Status = ManagerStatus.Initializing;

            Messenger.AddListener(AdvEvent.SHOW_BANNER, OnShowBanner);
            Messenger.AddListener(AdvEvent.SHOW_REWARDED, OnShowRewarded);
            Messenger.AddListener(AdvEvent.SHOW_INTERSTITIAL, OnShowInterstitial);

            Status = ManagerStatus.Started;
            Debug.Log("StuckManager: started");
        }

        private void OnShowBanner()
        {
            bool hasGoogleSubscription = Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB);
            if (!hasGoogleSubscription)
            {
                try
                {
                    Managers.BannerAdExample.LoadBanner();
                    Managers.BannerAdExample.ShowBannerAd();
                }
                catch (Exception ex)
                {
                    Debug.Log("ShowAdManager ex: " + ex);
                }
            }
        }

        private void OnShowRewarded()
        {
            bool hasGoogleSubscription = Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB);
            if (!hasGoogleSubscription)
            {
                try
                {
                    Managers.RewardedAdsButton.LoadAd();
                    Managers.RewardedAdsButton.ShowAd();
                }
                catch (Exception ex)
                {
                    Debug.Log("ShowAdManager ex: " + ex);
                }
            }
        }


        private void OnShowInterstitial()
        {
            bool hasGoogleSubscription = Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB);
            if (!hasGoogleSubscription)
            {
                try
                {
                    Managers.InterstitialAdExample.LoadAd();
                    Managers.InterstitialAdExample.ShowAd();
                }
                catch (Exception ex)
                {
                    Debug.Log("ShowAdManager ex: " + ex);
                }
            }
        }
    }
}