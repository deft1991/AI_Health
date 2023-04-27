using System.Collections;
using UnityEngine;
using Google.Play.Review;

namespace global.manager
{
    public class GoogleReviewManager : MonoBehaviour, IGameManager
    {

        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;

        private int _launchCount;
        
        public ManagerStatus Status { get; private set; }

        public void Startup()
        {
            Debug.Log("GoogleReviewManager starting...");
            Status = ManagerStatus.Initializing;

            _launchCount = PlayerPrefs.GetInt("TimesLaunched", 0);
            _launchCount++;
            PlayerPrefs.SetInt("TimesLaunched", _launchCount);
            Debug.Log("GoogleReviewManager: TimesLaunched: " + _launchCount);

            if (_launchCount > 1 && _launchCount % 2 == 0 && _launchCount <= 10)
            { 
                StartCoroutine(RequestReviews());
            }
            
            Status = ManagerStatus.Started;
            Debug.Log("GoogleReviewManager: started");
        }

        public void RequestGoogleReview()
        {
            StartCoroutine(RequestReviews());
        }

        private IEnumerator RequestReviews()
        {
            _reviewManager = new ReviewManager();
            
            /*
             * Request a Review Info Object
             * Follow the guidance about when to request in-app reviews
             * to determine good points in your app's user flow to prompt
             * the user for a review
             * (for example, after a user dismisses the summary screen at the end of a level in a game).
             * When your app gets close one of these points,
             * use the ReviewManager instance to create an async operation,
             * as shown in the following example:
             */
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }
            _playReviewInfo = requestFlowOperation.GetResult();
            Debug.Log("GoogleReviewManager: Got review");
            
            /*
             * Launch the in-app review flow
             * After your app receives the PlayReviewInfo instance,
             * it can launch the in-app review flow.
             * Note that the PlayReviewInfo object is only valid for a limited amount of time,
             * so your app should not wait too long before launching a flow.
             */
            
            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null; // Reset the object
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }
            /*
             * The flow has finished. The API does not indicate whether the user
             * reviewed or not, or even whether the review dialog was shown. Thus, no
             * matter the result, we continue our app flow.
             */
                        
        }
    }
}