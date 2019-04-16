/* Mitchell Hewitt 
 * Wed, Feb 27th, 2019 
 * Confirmation Popup*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationPopup : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject popUpCanvas;

    [SerializeField] int buildIndex;

    private void Start()
    {
        popUpCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OpenMenu()
    {
        popUpCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void CloseMenu()
    {
        popUpCanvas.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    public void Confirm()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0); //Change the index int to match the Menu scene index.
            popUpCanvas.SetActive(false);
        }
    }
}
