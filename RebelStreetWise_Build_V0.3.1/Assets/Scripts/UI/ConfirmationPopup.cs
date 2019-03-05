/* Mitchell Hewitt 
 * Wed, Feb 27th, 2019 
 * Confirmation Popup*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationPopup : MonoBehaviour
{
    Canvas returnCanvas;
    Canvas baseCanvas;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    void OpenMenu()
    {
        this.gameObject.SetActive(true);
    }
    
    void CloseMenu()
    {
        this.gameObject.SetActive(false);
        returnCanvas.gameObject.SetActive(true);
    }

    void Confirm()
    {
        SceneManager.LoadScene(1); //Change the index int to match the Menu scene index.
        this.gameObject.SetActive(false);
        baseCanvas.transform.GetChild(5).gameObject.SetActive(true);
    }
}
