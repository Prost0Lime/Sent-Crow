using UnityEngine;
using System.Collections;

public class myLog : MonoBehaviour
{
    public string MyLog;
    Queue myLogQueue = new Queue();

    void Start()
    {
        Debug.Log("Sent-Crow");
        Debug.Log("v.0.032");
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
        MyLog = logString;
        string newString = "\n [" + type + "] : " + MyLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        MyLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            MyLog += mylog;
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUILayout.Label(MyLog);
    }

}