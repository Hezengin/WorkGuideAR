using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;
using Assets.MyFolder.Scripts;
using UnityEngine.SceneManagement;

public class GuideManager : MonoBehaviour
{
    public TextMeshPro titleText;
    public TextMeshPro stepDescriptionText;
    public TextMeshPro substepDescriptionText;
    public GameObject nextButton;
    public GameObject previousButton;

    private JArray stepsArray;
    public static int currentStepIndex { get; private set; }
    public static Guide guide = new Guide();

    private NavigationManager navigationManager;
    private SessionManager sessionManager;

    #region GuideFunctions
    void Start()
    {
        sessionManager = new SessionManager();
        currentStepIndex = 0;
        SetGuideManual();

        if (string.IsNullOrEmpty(guide.manualPath))
        {
            Debug.LogError("Manual path is empty. Exiting application.");
            new ApplicationQuitter().QuitApplication();
            return;
        }

        LoadManual();
    }

    public void SetGuideManual()
    {
        if (guide != null)
        {
            Debug.Log($"Manual Choice and Level: {guide.manualChoice} - {guide.level}");
            guide.manualPath = $"{guide.manualChoice}_{guide.level}";
            Debug.Log($"Created Manual Path: {guide.manualPath}");
            return;
        }
        guide = new Guide();
    }

    private void LoadManual()
    {
        string jsonString = JsonUtil.LoadJsonFile(guide.manualPath);
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON string is empty. Check file path or content.");
            return;
        }

        JObject manual = JsonUtil.ParseJsonString(jsonString);
        InitializeManual(manual);
    }

    private void InitializeManual(JObject manual)
    {
        titleText.text = JsonUtil.GetValueFromPacket(manual, "manualTitle") ?? "Untitled";
        stepsArray = JsonUtil.GetStepsArray(manual);

        if (stepsArray != null && stepsArray.Count > 0)
        {
            navigationManager = new NavigationManager(currentStepIndex, stepsArray, stepDescriptionText,
                substepDescriptionText, nextButton, previousButton);
            navigationManager.UpdateStep();
            navigationManager.UpdateButtonStates();
        }
        else
        {
            Debug.LogError("No steps found in the manual.");
        }
    }
    #endregion

    #region OnClicks
    public void OnResetClicked()
    {
        currentStepIndex = 0;
        navigationManager.UpdateStep();
        navigationManager.UpdateButtonStates();
    }

    public void OnSaveClicked()
    {
        Session session = new Session();

        session.guide = guide;
        session.step = currentStepIndex + 1;

        if (session.guide.level == null || session.guide.manualChoice == null)
        {
            Debug.Log("The Guide of the session is not set correctly.");
            return;
        }

        session.CreateSessionFile();

        if (sessionManager.GetSessions() != null)
        {
            Debug.Log($"LISTCOUNT: {sessionManager.GetSessions().Count}");
            sessionManager.GetSessions().Add(session);
            Debug.Log($"LISTCOUNT: {sessionManager.GetSessions().Count}");
        }
        else
        {
            Debug.LogWarning("LIST IS NULL NOT INITIALIZED");
        }

        Debug.Log("Selected session attibutes that are saved: " + session.name + " - " + session.step);
        Debug.Log(session.guide.manualChoice + " - " + session.guide.level);

        sessionManager.AddSessionToList(session);
        SceneManager.LoadScene("SessionsScene");
    }

    public void OnHomeClicked()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnPreviousClicked()
    {
        navigationManager.PreviousStep();
    }

    public void OnNextClicked()
    {
        navigationManager.NextStep();
    }
    #endregion
}
