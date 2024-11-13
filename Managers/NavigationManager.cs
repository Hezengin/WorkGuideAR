using Assets.MyFolder.Scripts;
using Newtonsoft.Json.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    private int currentStepIndex;
    private JArray stepsArray;
    private TextMeshPro stepDescriptionText;
    private TextMeshPro substepDescriptionText;
    private GameObject nextButton;
    private GameObject previousButton;

    public NavigationManager(int currentStepIndex, JArray stepsArray, TextMeshPro stepDescriptionText, 
        TextMeshPro substepDescriptionText, GameObject nextButton, GameObject previousButton)
    {
        this.currentStepIndex = currentStepIndex;
        this.stepsArray = stepsArray;
        this.stepDescriptionText = stepDescriptionText;
        this.substepDescriptionText = substepDescriptionText;
        this.nextButton = nextButton;
        this.previousButton = previousButton;
    }

    public void NextStep()
    {
        if (currentStepIndex < stepsArray.Count - 1)
        {
            currentStepIndex++;
            UpdateStep();
            UpdateButtonStates();
        }
    }

    public void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            currentStepIndex--;
            UpdateStep();
            UpdateButtonStates();
        }
    }

    public void UpdateStep()
    {
        var step = stepsArray[currentStepIndex] as JObject;
        if (step != null)
        {
            stepDescriptionText.text = JsonUtil.GetValueFromPacket(step, "description");
            UpdateSubsteps(step);
        }
        else
        {
            Debug.LogError($"No step found at index {currentStepIndex}.");
        }
    }

    private void UpdateSubsteps(JObject step)
    {
        var substeps = step["substeps"] as JArray;
        substepDescriptionText.text = substeps != null ? string.Join("\n", substeps.Select(s => $"<alpha=#88>- {s}")) : string.Empty;
    }

    public void UpdateButtonStates()
    {
        nextButton.SetActive(currentStepIndex < stepsArray.Count - 1);
        previousButton.SetActive(currentStepIndex > 0);
    }
}
