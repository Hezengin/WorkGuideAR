using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System;

public class ApplicationQuitter : MonoBehaviour
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr hModule);

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #elif UNITY_UWP
            IntPtr moduleHandle = GetModuleHandle("UnityPlayer.dll");
        
            if (moduleHandle != IntPtr.Zero)
            {
                FreeLibrary(moduleHandle);
        
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        #else
            Application.Quit();
        #endif
    }
}
