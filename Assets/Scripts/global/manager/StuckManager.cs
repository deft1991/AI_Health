using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace global
{
    public class StuckManager : MonoBehaviour, IGameManager
    {
        public float waitSec = 3F;
        public ManagerStatus Status { get; private set; }

        public void Startup()
        {
            Debug.Log("Mission manager starting...");

            Status = ManagerStatus.Initializing;

            StartCoroutine(WaitForASec(waitSec));

            Status = ManagerStatus.Started;
            Debug.Log("FillDataManager: started");
        }

        public IEnumerator WaitForASec(float sec)
        {
            yield return new WaitForSeconds(sec);
        }
    }
}