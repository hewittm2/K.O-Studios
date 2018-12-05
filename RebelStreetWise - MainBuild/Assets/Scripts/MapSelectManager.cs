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
    public string selectedMap;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            //add functionality to the slider and the button in the stage select sceen
            mapSlector = GameObject.FindWithTag("MapSelector").GetComponent<Slider>();
            mapSelectButton = GameObject.FindWithTag("MapButton").GetComponent<Button>();
            mapSlector.onValueChanged.AddListener(delegate { StageSelect(); });
            mapSelectButton.onClick.AddListener(StageSelect);
        }
    }


    //void for when the value of the slider changes or when the button is selected
    public void StageSelect()
    {
        int q = Mathf.RoundToInt(mapSlector.value);
        Debug.Log(q);

        if (Input.GetButtonDown("A_1"))
        {
            Debug.Log("yes");
            selectedMap = stageNames[q];
            SceneManager.LoadScene(selectedMap);
        }
    }
}
