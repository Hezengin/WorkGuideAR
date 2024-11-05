using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ApplicationQuit : MonoBehaviour
{
    public void QuitApplication()
    {
        #if UNITY_EDITOR
        // Stop play mode in the Unity Editor
        EditorApplication.isPlaying = false;
        #else
        // Quit the app in a build
        Application.Quit();
        #endif
    }
}
