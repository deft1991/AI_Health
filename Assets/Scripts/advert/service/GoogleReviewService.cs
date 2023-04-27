using System;
using advert.@event;
using global;
using global.inapp;
using UnityEngine;
using UnityEngine.UI;

namespace Advert.service
{
    public class GoogleReviewService : MonoBehaviour
    {
        
        private int _launchCount;
        
        public void OnClickGoogleReview()
        {
            Managers.GoogleReviewManager.RequestGoogleReview();
        }
    }
}