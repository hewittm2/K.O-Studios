using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    CharacterSpawning characterSpawning;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Options(GameObject OptionsMenu)
    {
        OptionsMenu.SetActive(true);
    }
    public void ReturnCanvas(GameObject OptionsMenu)
    {
        OptionsMenu.SetActive(false);
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
