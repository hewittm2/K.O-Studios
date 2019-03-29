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

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
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
    }
    private void UpdateProgressUI (float progress)
    {
        loadingSlider.value = progress;
    }
}
