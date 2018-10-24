// Jake P. & Chris B.
// 10/24/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerAlpha : MonoBehaviour {

    private static GameManagerAlpha gma = null;

    public GameObject[] players = new GameObject[4];
    public GameObject[] characters;

    public string[] characterNames = new string[4];

    public static GameManagerAlpha Gma
    {
        get
        {
            return gma;
        }
    }

    private void Awake()
    {
        // Loads the list of character prefabs in to an array
        characters = Resources.LoadAll<GameObject>("Characters");

        if (gma != null && gma != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            gma = this;

        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("FighterTest");
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            SceneManager.LoadScene("CharacterSpawnTest");
        }
    }

    public void AssignCharacterToPlayer(int characterIndex)
    {
        if (Input.GetButtonDown("A_1"))
        {
            players[0] = characters[characterIndex];
        }

        if
        (Input.GetButtonDown("A_2"))
        {
            players[1] = characters[characterIndex];
        }

        if (Input.GetButtonDown("A_3"))
        {
            players[2] = characters[characterIndex];
        }

        if (Input.GetButtonDown("A_4"))
        {
            players[3] = characters[characterIndex];
        }
    }

    #region Scene Switching
    //UI Button Scene Switching (Scene Name)
    public void SceneSwitchButtonString(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    //UI Button Scene Switching (Scene Build Index Number)
    public void SceneSwitchButtonIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
}
