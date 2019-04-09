using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

    public Toggle team1win1;
    public GameObject team1win1Image;
    public Toggle team1win2;
    public GameObject team1win2Image;
    public Toggle team2win1;
    public GameObject team2win1Image;
    public Toggle team2win2;
    public GameObject team2win2Image;

    RoundManager roundManager;

    Scene scene;

    private static RoundManager rm = null;
    public static RoundManager Rm { get { return rm; } }

    private void Awake()
    {

        if (rm != null && rm != this)
        {
            //If so, destroy this one
            Destroy(this.gameObject);
            return;
        }
        else
        {
            //Else, make this the Main CSM
            rm = this;
        }

        scene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(this);
    }
    void OnSceneLoaded(LoadSceneMode mode)
    {
        if (scene.name != SceneManager.GetActiveScene().name)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateProperties()
    {

    }
}
