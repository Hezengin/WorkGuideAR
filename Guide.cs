
using System;

/// <summary>
/// Data class that contains the attributes for a Guide. 
/// TO DO: think about why this class is not serializable.
/// Answer to TO DO: 
/// </summary>

[Serializable]
public class Guide
{
    public Manual? manualChoice { get; set; }

    public Level? level { get; set; }

    public string manualPath { get; set; }

    public Guide(string manualPath)
    {
        this.manualPath = manualPath;
    }

    public Guide()
    {
        this.manualChoice = null;
        this.level = null;
        this.manualPath = null;
    }
}

[Serializable]
public enum Level
{
    BEGINNER = 0,
    EXPERIENCED = 1
}

[Serializable]
public enum Manual
{
    SMART_PROBE_SENSOR_MANUAL = 0,
    CREST_KAST_MANUAL = 1
}