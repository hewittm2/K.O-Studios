//Created by Leo and Brian 11 14 18
//Edited by
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayManager : MonoBehaviour {

    public GameObject popup;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // This function is to return to the main menu
    public void ReturnOnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Closes overlay to continue game
    public void ContinueOnClick()
    {
        popup.SetActive(false);
    }
  

}
