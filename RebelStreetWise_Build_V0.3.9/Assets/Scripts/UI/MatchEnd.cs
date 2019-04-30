using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchEnd : MonoBehaviour {

    FighterClass fighterClass;
    public GameObject EndCanvas;
    public GameObject Event1;
    public GameObject Event3;

    public bool canEnd = true;

	public void Winner (int teamNumber)
    {
        StartCoroutine(MatchEnder(teamNumber));
        //Creats slow motion
        Time.timeScale = 0.5f;
    }
    private IEnumerator MatchEnder(int teamNmber)
    {
        canEnd = false;
        yield return new WaitForSeconds(3);
        //Brings Up the Match End Screen and stops time so other people cannot move
        Time.timeScale = 0;
        if (teamNmber == 1){
            Event1.SetActive(true);
        }
        if (teamNmber == 2){
            Event3.SetActive(true);
        }
        EndCanvas.SetActive(true);
        Time.timeScale = 1;
    }
}
