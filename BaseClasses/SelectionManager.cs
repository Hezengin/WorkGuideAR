using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Base class for Managers 
/// </summary>
public abstract class SelectionManager : MonoBehaviour
{
    public GameObject actionButton;

    public abstract void SelectItem(int selectedItem);

    public void LoadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }
        Debug.LogError("SCENE: sceneName is null");
    }

    public void SelectionOff()
    {
        ResetSelection();
        actionButton.SetActive(false);
    }

    protected abstract void ResetSelection();
}
