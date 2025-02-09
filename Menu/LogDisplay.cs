using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogDisplay : MonoBehaviour
{
    public TextMeshProUGUI logText; // Drag the UI Text element here in the inspector

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
        if (type == LogType.Error)
        {
            // Append the new error message to the existing text
            logText.text += logString + "\n";
        }
    }
}