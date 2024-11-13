using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[Serializable]
public class Session
{
    public string name { get; set; } // the name of the session when it was cut off or saved
    public int step { get; set; } // the step where the session is cut off or saved
    public string time { get; } // the time where the session is cut off or saved
    public bool isSaved { get; set; } // boolean value to determine if a session was cut off or saved
    public Guide guide { get; set; }

    public Session()
    {
        this.time = FormatTimeAndDate();
        this.name = "Session_";
    }

    private string FormatTimeAndDate()
    {
        DateTime time = DateTime.Now;
        string str = time.ToString("yyyy-MM-dd HH:mm:ss");
        return str;
    }

    // Creates the session file and writes the session data to it
    public void CreateSessionFile()
    {
        string sessionOutput = JsonConvert.SerializeObject(this, Formatting.Indented); ;
        string directoryPath = Path.Combine(Application.dataPath, "Sessions");

        DateTime time = DateTime.Now;

        // Create the directory if it doesnt exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Format the time for the filename
        string formattedTime = time.ToString("yyyyMMdd_HHmmss");
        string filePath = Path.Combine(directoryPath, $"{name + formattedTime}.json");

        try
        {
            File.WriteAllText(filePath, sessionOutput);
            Debug.Log($"Session file created at: {filePath}");
            Debug.Log($"Contents of session file: {sessionOutput}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error creating session file: {e}");
        }
    }
}
