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

    public bool team1win1bool;
    public bool team1win2bool;
    public bool team2win1bool;
    public bool team2win2bool;

    public RoundManager[] otherRM;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(this);
        UpdateProperties();
    }

    void OnSceneLoaded(Scene sceneName, LoadSceneMode mode)
    {
        if (scene.name != SceneManager.GetActiveScene().name)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateProperties()
    {
        otherRM = FindObjectsOfType<RoundManager>();
        print(otherRM.Length);
        if(otherRM.Length > 1)
        {
            RoundManager notThis = otherRM[0];
            GameObject destroyThis = notThis.gameObject;
            Destroy(destroyThis);
        }
    }
    private void Update()
    {
        if (team1win1.isOn == true)
        {
            team1win1bool = true;
        }
        if (team1win2.isOn == true)
        {
            team1win2bool = true;
        }
        if (team2win1.isOn == true)
        {
            team2win1bool = true;
        }
        if (team2win2.isOn == true)
        {
            team2win2bool = true;
        }
    }

    public void bools()
    {
        if (team1win1.isOn == true)
        {
            string team1win1string = team1win1bool.ToString();
            PlayerPrefs.SetInt(team1win1string, team1win1bool ? 1 : 0);
            GetBool(team1win1string);
        }
        if (team1win2.isOn == true)
        {
            string team1win2string = team1win2bool.ToString();
            PlayerPrefs.SetInt(team1win2string, team1win2bool ? 1 : 0);
            GetBool(team1win2string);
        }
        if (team2win1.isOn == true)
        {
            string team2win1string = team2win1bool.ToString();
            PlayerPrefs.SetInt(team2win1string, team2win1bool ? 1 : 0);
            GetBool(team2win1string);
        }
        if (team2win2.isOn == true)
        {
            string team2win2string = team2win2bool.ToString();
            PlayerPrefs.SetInt(team2win2string, team2win2bool ? 1 : 0);
            GetBool(team2win2string);
        }
    }
    public static bool GetBool(string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
