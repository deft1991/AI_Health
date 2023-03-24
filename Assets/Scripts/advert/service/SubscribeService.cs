using System;
using advert.@event;
using global;
using global.inapp;
using UnityEngine;
using UnityEngine.UI;

namespace Advert.service
{
    public class SubscribeService : MonoBehaviour
    {
        [SerializeField] private Button removeAdvButton;

        #region MyRegion

        private void Awake()
        {
            Messenger<string>.AddListener(IapEvent.BUY_SUCCESSFUL, OnSuccessfulBuy);
        }


        private void OnSuccessfulBuy(string producId)
        {
            if (Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB))
            {
                removeAdvButton.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            removeAdvButton.onClick.AddListener(OnClickRemoveAdv);
            if (Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB))
            {
                removeAdvButton.gameObject.SetActive(false);
            }
        }

        #endregion PRIVATE

        #region PUBLIC

        public void OnClickRemoveAdv()
        {
            Managers.MyIAPManager.BuyNoAds();
            if (Managers.MyIAPManager.hasGoogleSubscription(MyIAPManager.TEST_SUB))
            {
                removeAdvButton.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}