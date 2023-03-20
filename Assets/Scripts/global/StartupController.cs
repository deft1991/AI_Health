using UnityEngine;
using UnityEngine.UI;

namespace global
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        private void Awake()
        {
            Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
            Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
        }

        private void OnDestroy()
        {
            Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
            Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
        }

        private void OnManagersProgress(int numReady, int numModules)
        {
            float progress = (float)numReady / numModules;
            /*
             * Update slider with load data
             */
            if (progressBar != null)
            {
                progressBar.value = progress;
            }
        }

        private void OnManagersStarted()
        {
            /*
             * Load next scene
             */
            Managers.FillDataManager.LoadFillDataScene();
        }
    }
}