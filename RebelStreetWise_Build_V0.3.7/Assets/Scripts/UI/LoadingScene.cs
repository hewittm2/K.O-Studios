
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    public Slider loadingSlider;
    private AsyncOperation operation;
    private Canvas canvas;

    private static LoadingScene _instance;
    public static LoadingScene Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        canvas = GetComponentInChildren<Canvas>(true);
        DontDestroyOnLoad(gameObject);
    }
    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("Team1RoundWins", 0);
        PlayerPrefs.SetInt("Team2RoundWins", 0);
    }
    public void LoadScene(string sceneName)
    {
        string resetWins = SceneManager.GetActiveScene().name;
        if (resetWins != "FighterTest")
            ResetPrefs();

        UpdateProgressUI(0);
        canvas.gameObject.SetActive(true);

        StartCoroutine(BeginLoad(sceneName));
    }
    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }
        UpdateProgressUI(operation.progress);
        operation = null;
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void UpdateProgressUI (float progress)
    {
        loadingSlider.value = progress;
    }
}
