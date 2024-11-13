using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using MixedReality.Toolkit.UX;

public class SessionManager : SelectionManager
{
    public GameObject buttonPrefab;
    public Transform sessionListContainer;

    private static List<Session> sessions;
    public Session selectedSession { get; set; } // the item from the list that is selected
    private bool isToggled = false;

    private void Start()
    {
        if (sessions != null)
        {
            foreach (Session session in sessions)
            {
                CreateSessionListItem(session);
            }
        }
    }

    public void AddSessionToList(Session session)
    {
        // Init list 
        if (sessions == null)
        {
            sessions = new List<Session>();
        }

        if (session != null)
        {
            // Add the session to the list and create an item from it
            selectedSession = session;
            sessions.Add(selectedSession);
            SceneManager.LoadScene("SessionsScene");
            Debug.Log($"Sessions: {sessions.Count}");
            return;
        }
        Debug.Log("Session null, could not add");
    }

    private void CreateSessionListItem(Session session)
    {
        // Instantieer een nieuwe knop binnen het Content-object    
        GameObject sessionButton = Instantiate(buttonPrefab, sessionListContainer);

        // Stel de tekst en functionaliteit in voor de nieuwe knop
        TextMeshProUGUI textComponent = sessionButton.transform.Find("Frontplate/AnimatedContent/Text").GetComponent<TextMeshProUGUI>();
        textComponent.text = session.name + session.time + "\n" +
            "<size=6><alpha=#88>" + session.guide.manualChoice.ToString() + " - " + session.guide.level.ToString() + "\n" +
            "Stap: " + session.step + "</size>";


        isToggled = !isToggled; // Toggle the state
        if (isToggled)
        {
            SetSession(session); // Action when toggled ON
        }
        else
        {
            SelectItem(0); // Action when toggled OFF
        }

        sessionButton.GetComponent<PressableButton>().OnClicked.AddListener(() => SetSession(session));
        Debug.Log("The session item is created.");
    }

    // Loading a Scene
    public void SetSession(Session session)
    {
        isToggled = !isToggled; // Toggle the state
        if (isToggled)
        {
            GuideManager.guide = session.guide;
            selectedSession = session;
            SelectItem(0);
        }
        else
        {
            selectedSession = session;
            ResetSelection(); // Action when toggled OFF
        }
    }

    public List<Session> GetSessions()
    {
        return sessions;
    }

    public override void SelectItem(int selectedItem)
    {
        actionButton.SetActive(true);
    }

    protected override void ResetSelection()
    {
        actionButton.SetActive(false);
    }

    // Function for back button UI
    public void OnHomeClicked()
    {
        LoadScene("StartScene");
    }

    public void OnNextClicked()
    {
        sessions.Remove(selectedSession);
        Debug.Log($"Sessions: {sessions.Count}");
        LoadScene("GuideScene"); 
    }
}
