using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : SelectionManager
{
    public GameObject nextButton;

    private void Start()
    {
        GuideManager.guide = new Guide();
    }

    public void SelectGuide(int selectedGuide)
    {
        SetSelection((Manual?)selectedGuide, GuideManager.SetManualChoice, nextButton);
    }

    public void OnNextClicked()
    {
        LoadScene("LevelSelectScene");
    }

    public void OnSessionClicked()
    {
        SceneManager.LoadScene("SessionsScene");
    }

    public void SelectionOff()
    {
        OnSelectionOff<Manual>(() => GuideManager.SetManualChoice(null), nextButton);
    }
}
