/* Mitchell Hewitt 
 * Wed, Feb 27th, 2019 
 * *Updated by Mitchell Hewitt: Friday, March 22nd, 2019
 * Confirmation Popup*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField]Canvas returnCanvas;
    [SerializeField]Canvas baseCanvas;
    
    public Canvas mainCanvas; 

    public int mainMenuIndex;
    public int mainMenuChildIndex;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        this.gameObject.SetActive(true);
    }
    
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
        returnCanvas.gameObject.SetActive(true);
    }

    public void Confirm()
    {
        SceneManager.LoadScene(mainMenuIndex); //Change the index int to match the Menu scene index.
        this.gameObject.SetActive(false);
        baseCanvas.transform.GetChild(mainMenuChildIndex).gameObject.SetActive(true);
    }
}
