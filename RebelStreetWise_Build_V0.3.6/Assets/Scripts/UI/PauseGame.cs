using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public GameObject event1;
    public GameObject event2;
    public GameObject event3;
    public GameObject event4;

    public GameObject pauseCanvas;
    public GameObject optionsCanvas;

  //  private Rigidbody[] rigidbodies;
 //   private CharacterController[] characterControllers;
    private FighterClass[] theFighters;

    void Start()
    {
   //     rigidbodies = FindObjectsOfType<Rigidbody>();
   //     characterControllers = FindObjectsOfType<CharacterController>();
        theFighters = FindObjectsOfType<FighterClass>();
    }
    public void Pause (int PlayerNum)
    {
        pauseCanvas.SetActive(true);

        foreach(FighterClass cc in theFighters)
        {
            cc.enabled = false;
        }
        Time.timeScale = 0;
        if (PlayerNum == 1)
        {
            event1.SetActive(true);
            event2.SetActive(false);
            event3.SetActive(false);
            event4.SetActive(false);
        }
        if (PlayerNum == 2)
        {
            event1.SetActive(false);
            event2.SetActive(true);
            event3.SetActive(false);
            event4.SetActive(false);
        }
        if (PlayerNum == 3)
        {
            event1.SetActive(false);
            event2.SetActive(false);
            event3.SetActive(true);
            event4.SetActive(false);
        }
        if (PlayerNum == 4)
        {
            event1.SetActive(false);
            event2.SetActive(false);
            event3.SetActive(false);
            event4.SetActive(true);
        }
    }

    public void ResumeGame ()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        foreach (FighterClass cc in theFighters)
        {
            cc.enabled = true;
        }
    }
    public void OptionsMenu()
    {
        optionsCanvas.SetActive(true);
    }
}
