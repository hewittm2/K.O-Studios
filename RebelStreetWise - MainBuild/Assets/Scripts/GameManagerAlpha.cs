// Jake P. & Chris B.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerAlpha : MonoBehaviour {

    private static GameManagerAlpha gma = null;

    public GameObject characterOne;
    public GameObject characterTwo;
    public GameObject characterThree;
    public GameObject characterFour;

    public static GameManagerAlpha Gma
    {
        get
        {
            return gma;
        }
    }

    private void Awake()
    {
        if (gma != null && gma != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            gma = this;

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {
		
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
