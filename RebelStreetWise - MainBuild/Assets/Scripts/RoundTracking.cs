//Jake Poshepny
//11 25 18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundTracking : MonoBehaviour
{
    [HideInInspector] public enum Round
	{
		One,
		Two,
		Three,
		Over
	}

	public Round currentRound;

    //Timer Variables
	public int roundTimer = 90;
	public int roundStartDelay = 3;
    private int midRoundStartDelay = 0;

    //UI Variables
    public Text currentRoundText;
	public Text roundTimerText;
	public Text roundCountdownText;
	public Text gameOverText;

    //Tracking Variables
    private int player1Health;
    private int player2Health;
    private int player3Health;
    private int player4Health;

    private int team1Health;
    private int team2Health;

    private int team1wins;
    private int team2wins;

    private void Start()
	{
		currentRound = Round.One;
		currentRoundText.text = "ROUND 1";

		StartCoroutine(RoundDelay());
        StartCoroutine(GetPlayerHealth());
	}

	private void Update()
	{
		if (currentRound == Round.Over)
		{
			//Put Game Over Sequence Here
			Debug.Log("Game Over");
		}
	}

    private IEnumerator GetPlayerHealth()
    {
        yield return new WaitForSeconds(1);

        player1Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().playerHealth);
        player2Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().playerHealth);
        player3Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().playerHealth);
        player4Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().playerHealth);

        team1Health = player1Health + player2Health;
        team2Health = player3Health + player4Health;

        team1wins = 0;
        team2wins = 0;
    }

	public void ChangeRound()
	{
        switch (currentRound)
		{
			case Round.One:
				currentRound = Round.Two;
				currentRoundText.text = "ROUND 2";
                ResetHealth();
				StartCoroutine(MidRoundDelay());
				break;
			case Round.Two:
				currentRound = Round.Three;
				currentRoundText.text = "ROUND 3";
                ResetHealth();
                StartCoroutine(MidRoundDelay());
				break;
			case Round.Three:
				currentRound = Round.Over;
				currentRoundText.text = "";
				gameOverText.text = "GAME OVER";
				SceneManager.LoadScene("CharacterSpawnTest");
				break;
		}
	}

    public void EndGame()
    {
        EndAnimation();
        //set canvas as active
        SceneManager.LoadScene("CharacterSpawnTest"); // <--- REMOVE THIS LATER
    }

    //Call when a player gets hit
    public void UpdateHealth()
    {
        player1Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().playerHealth);
        player2Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().playerHealth);
        player3Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().playerHealth);
        player4Health = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().playerHealth);

        team1Health = player1Health + player2Health;
        team2Health = player3Health + player4Health;

        if (player1Health <= 0 || player2Health <= 0)
        {
            team2wins++;
        }

        if (player3Health <= 0 || player4Health <= 0)
        {
            team1wins++;
        }
    }

    //Reset the health when a new Round starts
    private void ResetHealth()
    {
        GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().playerHealth = 100;
        GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().playerHealth = 100;
        GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().playerHealth = 100;
        GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().playerHealth = 100;

        player1Health = 1;
        player2Health = 100;
        player3Health = 100;
        player4Health = 100;

        team1Health = player1Health + player2Health;
        team2Health = player3Health + player4Health;
    }

	private IEnumerator RoundDelay()
	{
		for (int i = roundStartDelay; i >= -1; i--)
		{
			if (i > 0)
			{
				roundCountdownText.text = i.ToString();
				yield return new WaitForSeconds(1);
			}
			else if (i == 0)
			{
				roundCountdownText.text = "FIGHT";
				yield return new WaitForSeconds(1);
			}
			else if (i == -1)
			{
				roundCountdownText.text = "";
				StartCoroutine(RoundTimer());
			}
		}
	}

    private IEnumerator MidRoundDelay()
    {
        for (int i = midRoundStartDelay; i >= -1; i--)
        {
            if (i == 0)
            {
                roundCountdownText.text = "FIGHT";
                yield return new WaitForSeconds(1);
            }
            else if (i == -1)
            {
                roundCountdownText.text = "";
                StartCoroutine(RoundTimer());
            }
        }
    }

	private IEnumerator RoundTimer()
	{
		currentRoundText.gameObject.SetActive(true);
		roundTimerText.gameObject.SetActive(true);

		for (int i = roundTimer; i >= 0; i--)
		{
			roundTimerText.text = i.ToString();
			yield return new WaitForSeconds(1);

			if (i == 0 && currentRound != Round.Three)
			{
                if (team1Health > team2Health)
                {
                    team1wins++;
                    StartCoroutine(EndRound());
                }

                if (team1Health < team2Health)
                {
                    team2wins++;
                    StartCoroutine(EndRound());
                }

                if (team1Health == team2Health)
                {
                    EndRoundDraw();
                }
			}
		}
	}

    private IEnumerator EndRound()
    {
        if (team1wins == 2)
        {
            EndGame();
        }

        if (team2wins == 2)
        {
            EndGame();
        }

        else
        {
            //Stop Player Input
            currentRoundText.gameObject.SetActive(false);
            roundTimerText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1); //Round Change Delay
            ChangeRound();
        }
    }

    private void EndRoundDraw()
    {
        //Stop Player Input
        ResetHealth();
        StartCoroutine(MidRoundDelay());
    }

    private void EndAnimation()
    {
        //Put end animation here
    }
}
