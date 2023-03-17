using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue.util
{
    public class Logger : Singleton<Logger>
    {
        [SerializeField] private Text debugAreaText = null;

        [SerializeField] private bool enableDebug = false;

        [SerializeField] private int maxLines = 15;

        void Awake()
        {
            if (debugAreaText == null)
            {
                debugAreaText = GetComponent<Text>();
            }

            if (debugAreaText != null)
            {
                debugAreaText.text = string.Empty;
            }
        }

        void OnEnable()
        {
            if (debugAreaText != null)
            {
                debugAreaText.enabled = enableDebug;
                enabled = enableDebug;
            }
            else
            {
                enabled = false;
            }

            if (enabled)
            {
                debugAreaText.text +=
                    $"<color=\"white\">{DateTime.Now.ToString("HH:mm:ss.fff")} {this.GetType().Name} enabled</color>\n";
            }
        }

        public void Clear() => debugAreaText.text = string.Empty;

        public void LogInfo(string message)
        {
            if (debugAreaText != null)
            {
                ClearLines();
                debugAreaText.text += $"<color=\"green\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
            }
        }

        public void LogInfo(GameObject obj)
        {
            if (debugAreaText != null)
            {
                ClearLines();
                debugAreaText.text +=
                    $"<color=\"green\">{DateTime.Now.ToString("HH:mm:ss.fff")} Name: {obj.name} Id: {obj.GetHashCode()}</color>\n";
            }
        }

        public void LogError(string message)
        {
            if (debugAreaText != null)
            {
                ClearLines();
                debugAreaText.text += $"<color=\"red\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
            }
        }

        public void LogWarning(string message)
        {
            if (debugAreaText != null)
            {
                ClearLines();
                debugAreaText.text += $"<color=\"yellow\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
            }
        }

        private void ClearLines()
        {
            if (debugAreaText != null)
            {
                if (debugAreaText.text.Split('\n').Count() >= maxLines)
                {
                    debugAreaText.text = string.Empty;
                }
            }
        }
    }
}