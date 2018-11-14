//Created by: Brian Aronica 11/14/2018
//Edited by:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    //Changing scene to character select menu
    public void FightOnClick()
    {
        SceneManager.LoadScene("CharacterSpawnTest");
    }
    //Changing scene to training menu
    public void TrainingOnClick()
    {
        SceneManager.LoadScene("TrainingMenu", LoadSceneMode.Additive);
    }
    //Change scene to Models menu
    public void ModelsOnClick()
    {
        SceneManager.LoadScene("ModelsMenu", LoadSceneMode.Additive);
    }
    //Change scene to Options menu
    public void OptionOnClick()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
    }
    //Pulls up overlay to either exit program or go back to Main menu (Exits application until overlay is ready)
    public void ExitOnClick()
    {
        Application.Quit();
    }
    //This is for the highlighted sprites. Save this for the polishing sprint
    /*private void OnMouseOver()
    {
          
    }*/
}
