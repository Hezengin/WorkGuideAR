using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManager : SelectionManager
{
    public static GameObject buttonPrefab;
    public static Transform sessionListContainer;

    private static List<Session> sessions = new List<Session>();
    public static Session selectedSession = new Session(); // the item from the list that is selected

    private void Start()
    {
    }

    private void Update()
    {
    }

    public static void SaveSession()
    {
        selectedSession.guide = GuideManager.guide;
        selectedSession.step = GuideManager.currentStepIndex + 1;

        if (selectedSession.guide.level == null || selectedSession.guide.manualChoice == null)
        {
            Debug.Log("Guide of the session is not set correctly.");
            return;
        }

        selectedSession.CreateSessionFile();

        Debug.Log("Selected session attibutes that are saved: " + selectedSession.name + " - " + selectedSession.step);
        Debug.Log(selectedSession.guide.manualChoice + " - " + selectedSession.guide.level);

        // Voeg een nieuw item toe aan de sessielijst in het menu (Content van de ScrollView)
        AddSessionToList(selectedSession);
    }

    private static void AddSessionToList(Session session)
    {
        if (session != null && sessions != null)
        {
            // Add the session to the list and create an item from it
            sessions.Add(session);
            SceneManager.LoadScene("SessionsScene");
            Debug.Log($"Sessions: {sessions.Count}");
            CreateSessionListItem(session);
            return;
        }
        Debug.Log("Session null, could not add");
    }

    private static void CreateSessionListItem(Session session)
    {
        // Instantieer een nieuwe knop binnen het Content-object    
        GameObject sessionButton = Instantiate(buttonPrefab, sessionListContainer);

        // Stel de tekst en functionaliteit in voor de nieuwe knop
        sessionButton.GetComponentInChildren<Text>().text = session.name + " - " + session.time +  "/n" +
            session.guide.manualChoice.ToString() + " - " + session.guide.level.ToString() + "/n" + 
            "Stap: " + session.step;

        sessionButton.GetComponent<Button>().onClick.AddListener(() => LoadSession(session));
    }

    // Loading a Scene
    public static void LoadSession(Session session)
    {
        GuideManager.guide = session.guide;
        SceneManager.LoadScene("GuideScene");
    }

    // Function for back button UI
    public void OnBackClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
