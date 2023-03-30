using TMPro;
using UnityEngine;

namespace util
{
    public class ApplicationVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text version;
        [SerializeField] private string prefix = "Version: ";

        private void Start()
        {
            version.text = prefix + Application.version;
        }
    }
}