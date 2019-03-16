using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void CharacterSelectMenu()
    {
        SceneManager.LoadScene("CharacterSpawnTest");
    }
    public void StageSelectMenu()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
