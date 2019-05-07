//Jake Poshepny
//5 3 19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationPopupV2 : MonoBehaviour
{
    //Reference for each Panel being accessed in this Script
    public GameObject PausePanel = null;
    public GameObject OPanel = null;
    public GameObject CSPanel = null;
    public GameObject SSPanel = null;
    public GameObject EGPanel = null;

    //Set to the default Pause Panel
    public GameObject activePanel = null;

    //List of Event System Game Objects for each Confirmation Popup Panel (List them IN ORDER!)
    public List<GameObject> OEvents = new List<GameObject>();
    public List<GameObject> CSEvents = new List<GameObject>();
    public List<GameObject> SSEvents = new List<GameObject>();
    public List<GameObject> EGEvents = new List<GameObject>();

    //Local List of the default Pause Menu Event Systems
    private List<GameObject> events = new List<GameObject>();

    //Script used to access the default Pause Menu Event Systems
    public PauseGame PG = null;

    private IEnumerator Start()
    {
        //If the active panel was not manually set, set it to the correct object
        if (activePanel == null)
            activePanel = PausePanel;

        yield return new WaitForSeconds(0);
        events.Add(PG.event1);
        yield return new WaitForSeconds(0);
        events.Add(PG.event2);
        yield return new WaitForSeconds(0);
        events.Add(PG.event3);
        yield return new WaitForSeconds(0);
        events.Add(PG.event4);

        Debug.Log("Events Added (Confirmation Popup V2)");
    }

    //Enable Options Panel
    public void OptionsEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].activeSelf)
            {
                ChangePanel(OPanel);
                events[i].SetActive(false);
                OEvents[i].SetActive(true);
            }
        }
    }

    //Enable Character Select Popup
    public void CSEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].activeSelf)
            {
                ChangePanel(CSPanel);
                events[i].SetActive(false);
                CSEvents[i].SetActive(true);
            }
        }
    }

    //Enable Stage Select Popup
    public void SSEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].activeSelf)
            {
                ChangePanel(SSPanel);
                events[i].SetActive(false);
                SSEvents[i].SetActive(true);
            }
        }
    }

    //Enable Exit Game Popup
    public void EGEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].activeSelf)
            {
                ChangePanel(EGPanel);
                events[i].SetActive(false);
                EGEvents[i].SetActive(true);
            }
        }
    }

    //Disable Options Panel
    public void OptionsDisable()
    {
        for (int i = 0; i < OEvents.Count; i++)
        {
            if (OEvents[i].activeSelf)
            {
                ChangePanel(PausePanel);
                OEvents[i].SetActive(false);
                events[i].SetActive(true);
            }
        }
    }

    //Disable Character Select Popup
    public void CSDisable()
    {
        for (int i = 0; i < CSEvents.Count; i++)
        {
            if (CSEvents[i].activeSelf)
            {
                ChangePanel(PausePanel);
                CSEvents[i].SetActive(false);
                events[i].SetActive(true);
            }
        }
    }

    //Disable Stage Select Popup
    public void SSDisable()
    {
        for (int i = 0; i < SSEvents.Count; i++)
        {
            if (SSEvents[i].activeSelf)
            {
                ChangePanel(PausePanel);
                SSEvents[i].SetActive(false);
                events[i].SetActive(true);
            }
        }
    }

    //Disable Exit Game Popup
    public void EGDisable()
    {
        for (int i = 0; i < EGEvents.Count; i++)
        {
            if (EGEvents[i].activeSelf)
            {
                ChangePanel(PausePanel);
                EGEvents[i].SetActive(false);
                events[i].SetActive(true);
            }
        }
    }

    private void ChangePanel(GameObject panel)
    {
        activePanel.SetActive(false);
        activePanel = panel;
        activePanel.SetActive(true);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
