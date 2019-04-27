//Updated Torrel L
//4/26/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchTimer : MonoBehaviour
{

    [Header("How Long The Round Lasts")]
    public int minTime;
    public int secTime;
    public Text minText;
    public Text secText;
    public Text countdownText;
    [Header("Display Which Team Won")]
    public Text winnerText;
    public GameObject team1Wins;
    public GameObject team2Wins;


    private FighterClass[] fighterHP;
    public List<FighterClass> theTeam1 = new List<FighterClass>();
    public List<FighterClass> theTeam2 = new List<FighterClass>();
   [SerializeField] private int Team1HP;
    [SerializeField] private int Team2HP;

    private IEnumerator Start ()
    {
        Debug.Log(PlayerPrefs.GetInt("Team1RoundWins"));
        if (PlayerPrefs.HasKey("Team1RoundWins") == false || PlayerPrefs.HasKey("Team2RoundWins") == false)
        {
            PlayerPrefs.SetInt("Team1RoundWins", 0);
            PlayerPrefs.SetInt("Team2RoundWins", 0);
            Debug.Log("T");
        }

        if (PlayerPrefs.GetInt("Team1RoundWins") == 1)
            team1Wins.SetActive(true);
        if (PlayerPrefs.GetInt("Team2RoundWins") == 1)
            team2Wins.SetActive(true);

        fighterHP = FindObjectsOfType<FighterClass>();
        foreach (FighterClass fic in fighterHP)
        {
            if (fic.teamNumber == 1)
                theTeam1.Add(fic);
            else
                theTeam2.Add(fic);

            fic.enabled = false;
        }


        secText.text = secTime.ToString();
        minText.text = minTime.ToString() + ":";

        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Fight!";
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "";
        foreach (FighterClass fighter in fighterHP)
        {
            fighter.enabled = true;
        }
        StartCoroutine(MatchTimerE());
    }

	private IEnumerator MatchTimerE()
    {
        yield return new WaitForSeconds(1);
        secTime -= 1;
        if (secTime < 0 && minTime > 0)
        {
            minTime -= 1;
            secTime = 59;
        }
        secText.text = secTime.ToString();
        minText.text = minTime.ToString() + ":";

        if (secTime > 0 || minTime > 0)
        {
            StartCoroutine(MatchTimerE());
        }
        else
          RoundEnd();
    }
    public void RoundEnd()
    {
        StopCoroutine(MatchTimerE());
        MatchEnd EndGame = FindObjectOfType<MatchEnd>();
        int health1 = theTeam1[0].currentHealth;
        int health2 = theTeam1[1].currentHealth;
        int health3 = theTeam2[0].currentHealth;
        int health4 = theTeam2[1].currentHealth;

        Team1HP = theTeam1[0].currentHealth + theTeam1[1].currentHealth;
        Team2HP = theTeam2[0].currentHealth + theTeam2[1].currentHealth;

        if (Team1HP > Team2HP)
        {
            PlayerPrefs.SetInt("Team1RoundWins", PlayerPrefs.GetInt("Team1RoundWins") + 1);
            if (PlayerPrefs.GetInt("Team1RoundWins") == 2)
            {
                EndGame.Winner(1);
                return;
            }
            else
            {
                winnerText.text = "Team 1 wins the round!";
                StartCoroutine(RestartRound());
                return;
            }

        }
        if (Team2HP > Team1HP)
        {
            PlayerPrefs.SetInt("Team2RoundWins", PlayerPrefs.GetInt("Team2RoundWins") + 1);
            if (PlayerPrefs.GetInt("Team2RoundWins") == 2)
            {
                EndGame.Winner(2);
                return;
            }
            else
            {
                winnerText.text = "Team 2 wins the round!";
                StartCoroutine(RestartRound());
                return;
            }
        }
        if (Team1HP == Team2HP)
        {
            winnerText.text = "This round is a draw!";
            StartCoroutine(RestartRound());
        }
    }
    IEnumerator RestartRound ()
    {
        yield return new WaitForSeconds(1f);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
