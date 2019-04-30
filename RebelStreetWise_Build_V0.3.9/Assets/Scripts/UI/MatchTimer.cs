using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchTimer : MonoBehaviour {


    public int minTime;
    public int secTime;

    public Text minText;
    public Text secText;

    public Image winnerImage;
    public Sprite team1Wins;
    public Sprite team2Wins;

    private bool canSet1 = true;
    private bool canSet2 = true;

    private bool canCount = true;
    MatchEnd matchEnd;

    private IEnumerator Start () {
        if (secTime < 10)
        {
            secText.text = "0" + secTime.ToString();
        }
        else
        {
            secText.text = secTime.ToString();
        }
        if (minTime < 10)
        {
            minText.text = "0" + minTime.ToString();
        }
        else
        {
            minText.text = minTime.ToString();
        }
        FighterClass[] fighterClass = FindObjectsOfType<FighterClass>();
        foreach(FighterClass fighter in fighterClass)
        {
            fighter.enabled = false;
        }
        yield return new WaitForSeconds(3);
        foreach (FighterClass fighter in fighterClass){
            fighter.enabled = true;
        }
        StartCoroutine(matchTimer());
    }

	private IEnumerator matchTimer()
    {
        yield return new WaitForSeconds(1);
        secTime -= 1;
        if (secTime < 0){
            minTime -= 1;
            secTime = 59;
            if (minTime < 0)
            {
                secTime = 00;
                canCount = false;
                minTime = 0;
                RoundEnd();
            }
        }
        if(secTime < 10)
        {
            secText.text = "0" + secTime.ToString();
        }else{
            secText.text = secTime.ToString();
        }
        if (minTime < 10)
        {
            minText.text = "0" + minTime.ToString();
        }else{
            minText.text = minTime.ToString();
        }
        if(canCount == true)
        {
            StartCoroutine(matchTimer());
        }
    }
    public void RoundEnd()
    {
        FighterClass[] fighterClass = FindObjectsOfType<FighterClass>();

        int health1 = fighterClass[0].currentHealth;
        int health2 = fighterClass[1].currentHealth;
        int health3 = fighterClass[2].currentHealth;
        int health4 = fighterClass[3].currentHealth;

        int lowestHealth = Mathf.Min(health1, health2, health3, health4);

        FighterClass fc = FindObjectOfType<FighterClass>();
        MatchEnd matchEnd = FindObjectOfType<MatchEnd>();

        RoundManager roundManager = FindObjectOfType<RoundManager>();

        foreach (FighterClass fic in fighterClass)
        {
            if (fic.currentHealth == lowestHealth)
            {
                if (fic.teamNumber == 2 && canSet1 == true)
                {
                    canSet1 = false;
                    winnerImage.sprite = team1Wins;
                    if (roundManager.team1win1.isOn == true)
                    {
                        roundManager.team1win2.isOn = true;
                        roundManager.team1win2Image.SetActive(true);
                        matchEnd.Winner(fic.teamNumber);
                    }
                    else if (roundManager.team1win1.isOn == false)
                    {
                        roundManager.team1win1.isOn = true;
                        StartCoroutine(RestartRound());
                        roundManager.team1win1Image.SetActive(true);
                    }
                }
                if (fic.teamNumber == 1 && canSet2 == true)
                {
                    canSet2 = false;
                    winnerImage.sprite = team2Wins;
                    if (roundManager.team2win1.isOn == true)
                    {
                        roundManager.team2win2.isOn = true;
                        roundManager.team2win2Image.SetActive(true);
                        matchEnd.Winner(fic.teamNumber);
                    }
                    else if(roundManager.team2win1.isOn == false)
                    {
                        roundManager.team2win1.isOn = true;
                        StartCoroutine(RestartRound());
                        roundManager.team2win1Image.SetActive(true);
                    }
                }
                winnerImage.gameObject.SetActive(true);
                roundManager.UpdateProperties();
                roundManager.bools();
            }
        }
    }
    IEnumerator RestartRound ()
    {
        yield return new WaitForSeconds(4);
        matchEnd = FindObjectOfType<MatchEnd>();
        if(matchEnd.canEnd == true)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}
