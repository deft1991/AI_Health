using UnityEngine;

public class Utility : MonoBehaviour
{
    public static void SetCanvasGroupEnabled(CanvasGroup canvasGroup, bool enabled)
    {
        canvasGroup.alpha = enabled ? 1.0f : 0.0f;
        canvasGroup.interactable = enabled;
        canvasGroup.blocksRaycasts = enabled;
    }
}