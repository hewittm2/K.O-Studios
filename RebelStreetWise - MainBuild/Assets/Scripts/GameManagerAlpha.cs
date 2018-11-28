// Jake P. & Chris B.
// 10/24/18
//Edited by Ryan Van Dusen
//10/31/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManagerAlpha : MonoBehaviour {

    private static GameManagerAlpha gma = null;

    public GameObject[] players = new GameObject[4];
    //all characters
    public GameObject[] characters;
    //what characters are picked
    public string[] characterNames = new string[4];
    //Regions for character select menu
    public string[] CharacterSelectRegion = new string[] { "TopLeft", "TopMiddle", "TopRight", "BottomRight", "BottomMiddle", "BottomLeft" };
    //to store what region was picked
    public string player1Selected;
    public string player2Selected;
    public string player3Selected;
    public string player4Selected;
  

    //


    //Characte select UI Event system
    public EventSystem CharacterSelectUIEvent;
    


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

            //Player X selected from event system
            player1Selected = CharacterSelectUIEvent.currentSelectedGameObject.name;
            for(int i = 0; i < 6; i++)
            {   
                //runs what see what region was selected
                if(player1Selected == CharacterSelectRegion[i])
                {
                    //applies what character was selected
                    players[0] = characters[i];
                    //selects selected name
                    characterNames[0] = characters[i].name;
                  
                }
            }      
        }

        if
        (Input.GetButtonDown("A_2"))
        {
            player2Selected = CharacterSelectUIEvent.currentSelectedGameObject.name;
            for (int j = 0; j < 6; j++)
            {
                if (player2Selected == CharacterSelectRegion[j])
                {

                    players[1] = characters[j];
                    characterNames[1] = characters[j].name;
                }
            }
        }

        if (Input.GetButtonDown("A_3"))
        {
            player3Selected = CharacterSelectUIEvent.currentSelectedGameObject.name;
            for (int i = 0; i < 6; i++)
            {
                if (player1Selected == CharacterSelectRegion[i])
                {

                    players[2] = characters[i];
                    characterNames[2] = characters[i].name;
                }
            }
        }

        if (Input.GetButtonDown("A_4"))
        {
            player4Selected = CharacterSelectUIEvent.currentSelectedGameObject.name;
            for (int o = 0; o < 6; o++)
            {
                if (player4Selected == CharacterSelectRegion[o])
                {

                    players[3] = characters[o];
                    characterNames[3] = characters[o].name;
                }
            }
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
