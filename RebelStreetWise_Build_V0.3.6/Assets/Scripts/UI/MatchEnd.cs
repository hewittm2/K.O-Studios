using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchEnd : MonoBehaviour {

    FighterClass fighterClass;
    [SerializeField] private Text winnerText;
    public GameObject EndCanvas;
    public GameObject Event1;
    public GameObject Event3;

	public void Winner (int teamNumber)
    {
        StartCoroutine(MatchEnder(teamNumber));
        //Creats slow motion
        Time.timeScale = 0.5f;
    }
    private IEnumerator MatchEnder(int teamNmber)
    {
        // Displays the Text before the match ends
        if(teamNmber == 1){
            winnerText.text = "Team 2 Wins";
        }
        if (teamNmber == 2){
            winnerText.text = "Team 1 Wins";
        }
        yield return new WaitForSeconds(3);
        //Brings Up the Match End Screen and stops time so other people cannot move
        Time.timeScale = 0;
        EndCanvas.SetActive(true);
        if (teamNmber == 1){
            Event3.SetActive(true);
        }
        if (teamNmber == 2){
            Event1.SetActive(true);
        }
        Time.timeScale = 1;
    }
}
