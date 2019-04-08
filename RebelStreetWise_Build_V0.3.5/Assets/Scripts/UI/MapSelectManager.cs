using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour
{
    public Slider mapSlector;
    public Button mapSelectButton;

    public string[] stageNames;
    //current scene name selected
    private string selectedMap;

    public Image currentMapImage;
    public Sprite[] mapImages;

    private void Start()
    {
        int q = Mathf.RoundToInt(mapSlector.value);
        currentMapImage.sprite = mapImages[q];
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            //add functionality to the slider and the button in the stage select sceen
            mapSlector = GameObject.FindWithTag("MapSelector").GetComponent<Slider>();
            mapSelectButton = GameObject.FindWithTag("MapButton").GetComponent<Button>();
            int q = Mathf.RoundToInt(mapSlector.value);
            currentMapImage.sprite = mapImages[q];
        }
    }


    //void for when the value of the slider changes or when the button is selected
    public void StageSelect()
    {
        int q = Mathf.RoundToInt(mapSlector.value);

        selectedMap = stageNames[q];
        print(selectedMap);
        FindObjectOfType<LoadingScene>().LoadScene(selectedMap);
        
    }
}
