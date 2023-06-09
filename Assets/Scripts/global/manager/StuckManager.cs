using System.Collections;
using UnityEngine;

namespace global.manager
{
    public class StuckManager : MonoBehaviour, IGameManager
    {
        public float waitSec = 3F;
        public ManagerStatus Status { get; private set; }

        public void Startup()
        {
            Debug.Log("StuckManager starting...");

            Status = ManagerStatus.Initializing;

            StartCoroutine(WaitForASec(waitSec));
        }

        public IEnumerator WaitForASec(float sec)
        {
            yield return new WaitForSeconds(sec);
            Status = ManagerStatus.Started;
            Debug.Log("StuckManager: started");
        }
    }
}