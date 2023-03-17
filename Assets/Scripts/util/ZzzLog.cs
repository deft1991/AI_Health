using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ZzzLog : MonoBehaviour
{
    uint qsize = 30; // number of messages to keep
    Queue myLogQueue = new Queue();

    [SerializeField] private TMP_Text field;

    void Start()
    {
        // Debug.Log("Started up logging.");
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLogQueue.Enqueue("[" + type + "] : " + logString);
        if (type == LogType.Exception)
        {
            myLogQueue.Enqueue(stackTrace);
        }

        while (myLogQueue.Count > qsize)
        {
            myLogQueue.Dequeue();
        }

        field.text += "\n" + string.Join("\n", myLogQueue.ToArray());
    }

    // void OnGUI() {
    //     GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 600, Screen.height));
    //     GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()));
    //     GUILayout.EndArea();
    // }
}