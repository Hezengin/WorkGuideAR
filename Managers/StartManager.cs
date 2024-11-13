using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : SelectionManager
{
    public override void SelectItem(int selectedManual)
    {
        GuideManager.guide.manualChoice = (Manual)selectedManual;
        actionButton.SetActive(true);
    }

    protected override void ResetSelection() { GuideManager.guide.manualChoice = null; }
    public void OnNextClicked() { LoadScene("LevelSelectScene"); }
    public void OnSessionClicked() { LoadScene("SessionsScene"); }
}
