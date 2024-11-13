using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SelectionManager
{
    public override void SelectItem(int selectedLevel)
    {
        GuideManager.guide.level = (Level)selectedLevel;
        actionButton.SetActive(true);
    }

    protected override void ResetSelection() { GuideManager.guide.level = null; }
    public void OnStartClicked() { LoadScene("GuideScene"); }
    public void OnHomeClicked() { LoadScene("StartScene"); }
}
