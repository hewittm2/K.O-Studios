//Jake Poshepny
//4 30 19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //List of all UI Panels in the current scene
    public List<GameObject> panels = new List<GameObject>();

    //Holder for the default/currently active UI Panel
    public GameObject activePanel = null;

    private void Awake()
    {
        //Turn off all UI Panels
        foreach (GameObject p in panels)
            p.SetActive(false);

        //Turn on default UI Panel
        activePanel.SetActive(true);
    }

    //Switch current UI Panel
    public void ChangePanel(GameObject newPanel)
    {
        Debug.Log("LET ME IIIIIIIIIIIIIIIIIIIIIIN");
        activePanel.SetActive(false);
        activePanel = newPanel;
        activePanel.SetActive(true);
    }

    //Load Scene based on name string
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
