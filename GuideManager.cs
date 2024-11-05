using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;
using Assets.MyFolder.Scripts;
using System.Linq;
using UnityEngine.SceneManagement;

public class GuideManager : MonoBehaviour
{
    // Verbind de TextMeshPro objecten
    public TextMeshPro titleText;
    public TextMeshPro stepDescriptionText;  // Voor de stapbeschrijving
    public TextMeshPro substepDescriptionText; // Voor de substeps

    // Knoppen voor navigatie
    public GameObject nextButton;
    public GameObject previousButton;

    private JArray stepsArray;
    public static int currentStepIndex = 0;

    // Guide that will be filled by the other Manager classes
    public static Guide guide = new Guide();

    void Start()
    {
        // Setup for guide
        SetGuideManual();
        
        if (string.IsNullOrEmpty(guide.manualPath))
        {
            Debug.LogError("Manual path is empty. Exiting application.");
            
            ApplicationQuit applicationQuit = new ApplicationQuit();
            applicationQuit.QuitApplication();
            
            return;
        }

        LoadManual();
    }

    private void LoadManual()
    {
        string jsonString = JsonUtil.LoadJsonFile(guide.manualPath);
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON string is empty. Check file path or content.");
            return;
        }

        JObject manual = JObject.Parse(jsonString);
        InitializeManual(manual);
    }

    private void InitializeManual(JObject manual)
    {
        titleText.text = manual["manualTitle"]?.ToString() ?? "Untitled";
        //Debug.Log($"Title: {titleText.text}");

        stepsArray = manual["steps"] as JArray;
        if (stepsArray != null && stepsArray.Count > 0)
        {
            UpdateStep(currentStepIndex);
            UpdateButtonStates();
        }
        else
        {
            Debug.LogError("No steps found in the manual.");
        }
    }

    private void UpdateStep(int stepIndex)
    {
        var step = stepsArray[stepIndex] as JObject;
        if (step != null)
        {
            stepDescriptionText.text = JsonUtil.GetValueFromPacket(step, "description");
            UpdateSubsteps(step);
        }
        else
        {
            Debug.LogError($"No step found at index {stepIndex}.");
        }
    }

    private void UpdateSubsteps(JObject step)
    {
        var substeps = step["substeps"] as JArray;
        substepDescriptionText.text = substeps != null
            ? string.Join("\n", substeps.Select(s => $"<alpha=#88>- {s}"))
            : string.Empty;
    }

    // Functie voor het navigeren naar de volgende stap
    public void NextStep()
    {
        if (currentStepIndex < stepsArray.Count - 1)
        {
            currentStepIndex++;
            UpdateStep(currentStepIndex);
            UpdateButtonStates();
        }
    }

    // Functie voor het navigeren naar de vorige stap
    public void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            currentStepIndex--;
            UpdateStep(currentStepIndex);
            UpdateButtonStates();
        }
    }

    // Update de knopstatus (activeer of deactiveer knoppen afhankelijk van de huidige stap)
    void UpdateButtonStates()
    {
        nextButton.SetActive(currentStepIndex < stepsArray.Count - 1);
        previousButton.SetActive(currentStepIndex > 0);
    }

    // Setter for the type of manual
    public static void SetManualChoice(Manual? manualChoice)
    {
        if (manualChoice == null)
        {
            Debug.LogWarning("Guide choice is null.");
            return;
        }
        guide.manualChoice = manualChoice;
        Debug.Log($"Manual Choice: {guide.manualChoice}");

    }

    // Setter for the level of the guide
    public static void SetGuideLevel(Level? level)
    {
        if (level == null)
        {
            Debug.LogWarning("Guide level is null.");
            return;
        }
        guide.level = level;
        Debug.Log($"Manual Level: {guide.level}");
    }

    // Creater for the manualPath based on the manualChoice and the level.
    public void SetGuideManual()
    {
        if (guide == null)
        {
            guide = new Guide();
        }
        Debug.Log($"Manual Choice: {guide.manualChoice}");
        Debug.Log($"Manual Level: {guide.level}");
        guide.manualPath = $"{guide.manualChoice}_{guide.level}";
        Debug.Log($"Manual Path: {guide.manualPath}");
    }


    public void ResetManual()
    {
        currentStepIndex = 0;
        UpdateStep(currentStepIndex);
        UpdateButtonStates();
    }
    public void OnSaveClicked()
    {
        SessionManager.SaveSession();
    }

    public void ReturnToStartMenu()
    {
        Debug.Log("Returning to Home Page");
        SceneManager.LoadScene("StartScene");
    }
}
