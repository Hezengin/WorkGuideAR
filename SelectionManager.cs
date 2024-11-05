using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Base class for Managers that have at least:
/// - a gameObjcect button
/// - an action for the button to trigger
/// </summary>
public abstract class SelectionManager : MonoBehaviour
{
    
    protected void SetSelection<T>(T? selectedValue, System.Action<T?> setGuideAction, GameObject button) 
    {
        setGuideAction(selectedValue);
        button.SetActive(true);
        Debug.Log($"Selected value: {selectedValue.ToString()}");
    }

    protected void OnSelectionOff<T>(System.Action setGuideAction, GameObject button) 
    {
        setGuideAction();
        button.SetActive(false);
        Debug.Log("Selection off - button disabled");
    }

    protected void LoadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("No selection made");
        }
    }
}
