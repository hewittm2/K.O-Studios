using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject startMenu;

    public void PlayGame()
    {
        homeMenu.SetActive(true);
        startMenu.SetActive(false);
    }
}