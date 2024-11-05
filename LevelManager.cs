using UnityEngine;

public class LevelManager : SelectionManager
{
    public GameObject startButton;

    public void SelectLevel(int selectedLevel)
    {
        SetSelection((Level?)selectedLevel, GuideManager.SetGuideLevel, startButton);
    }

    public void OnStartClicked()
    {
        LoadScene("GuideScene");
    }

    public void OnBackClicked()
    {
        LoadScene("StartScene");
    }

    public void SelectionOff()
    {
        OnSelectionOff<Level>(() => GuideManager.SetGuideLevel(null), startButton);
    }
}
