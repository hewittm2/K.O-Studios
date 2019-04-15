using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    CharacterSpawning characterSpawning;
    public GameObject OptionsMenu;
    public GameObject ReturnMenu;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
        ReturnMenu.SetActive(false);
    }
    public void ReturnCanvas()
    {
        OptionsMenu.SetActive(false);
        ReturnMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CharacterMoveOn()
    {
        characterSpawning = FindObjectOfType<CharacterSpawning>();
        if(characterSpawning.players.Count == 4)
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
