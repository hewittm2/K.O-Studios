//Jake Poshepny
//11 25 18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundTracking : MonoBehaviour
{
	public enum Round
	{
		One,
		Two,
		Three,
		Over
	}

	private Round currentRound;

	public int roundTimer = 90;
	public int roundStartDelay = 3;
	private int midRoundStartDelay = 5;


	public Text currentRoundText;
	public Text roundTimerText;
	public Text roundCountdownText;
	public Text gameOverText;

	public float player1Health;
	public float player2Health;
	public float player3Health;
	public float player4Health;

	public float team1Health;
	public float team2Health;

	public int team1wins;
	public int team2wins;

	public List<GameObject> playerNum = new List<GameObject>();
	private CharacterSpawning spawnScript;
	public List<Transform> spawnList = new List<Transform>();

	private void Start()
	{
		spawnScript = FindObjectOfType<CharacterSpawning>();

		currentRound = Round.One;
		currentRoundText.text = "ROUND 1";

		for(int i = 1; i <= 4; i++)
		{
			spawnList.Add(GameObject.Find("Spawn Location " + i).transform);
		}

		for(int i = 1; i <= 4; i++)
		{
			playerNum[i-1] = GameObject.FindGameObjectWithTag("Player" + i);
			playerNum[i - 1].GetComponent<FighterClass>().enabled = false;
		}

		StartCoroutine(RoundDelay());
		//StartCoroutine(GetPlayerHealth());
	}

	private void Update()
	{
		if (currentRound == Round.Over)
		{
			//Put Game Over Sequence Here
			Debug.Log("Game Over");
		}
	}
	/* ===== Commented out for round start timer test =====
    private IEnumerator GetPlayerHealth()
    {
        yield return new WaitForSeconds(1);

        player1Health = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().health;
        player2Health = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().health;
        player3Health = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().health;
        player4Health = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().health;

        team1Health = player1Health + player2Health;
        team2Health = player3Health + player4Health;

        team1wins = 0;
        team2wins = 0;
    }
    */
	public void ChangeRound()
	{
		switch (currentRound)
		{
		case Round.One:
			currentRound = Round.Two;
			currentRoundText.text = "ROUND 2";
			//ResetHealth();
			StartCoroutine(MidRoundDelay());
			break;
		case Round.Two:
			currentRound = Round.Three;
			currentRoundText.text = "ROUND 3";
			//ResetHealth();
			StartCoroutine(MidRoundDelay());
			break;
		case Round.Three:
			currentRound = Round.Over;
			currentRoundText.text = "";
			gameOverText.text = "GAME OVER";
			//SceneManager.LoadScene("CharacterSpawnTest");
			break;
		}
	}

	public void EndGame()
	{
		EndAnimation();
		//set end of round canvas as active (deals with replaying)
		SceneManager.LoadScene("CharacterSpawnTest"); //Remove this later
	}

	//Call when a player gets hit
	public void UpdateHealth()
	{
		player1Health = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().health;
		player2Health = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().health;
		player3Health = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().health;
		player4Health = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().health;

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
	/* ========== Commented out for testing ==========
    //Reset the health when a new Round starts
    private void ResetHealth()
    {
        GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>().health = 100;
        GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>().health = 100;
        GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerInfo>().health = 100;
        GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerInfo>().health = 100;

        player1Health = 100;
        player2Health = 100;
        player3Health = 100;
        player4Health = 100;

        team1Health = player1Health + player2Health;
        team2Health = player3Health + player4Health;
    }
*/

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
				for (int ii = 0; ii < 4; ii++)
				{
					playerNum[ii].GetComponent<FighterClass>().enabled = true;
				}
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
				for (int ii = 0; ii < 4; ii++)
				{
					playerNum[ii].GetComponent<FighterClass>().enabled = true;
				}
				StartCoroutine(RoundTimer());
			}
			yield return new WaitForSeconds(1);
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
			{ int count = 0;
				foreach(GameObject g in playerNum)
				{

					g.transform.position = spawnList[count].transform.position;
					count++;

				}

				for (int ii = 0; ii < 4; ii++)
				{
					playerNum[ii].GetComponent<FighterClass>().enabled = false;
				}

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
		if (team1wins == 2 || team2wins == 2)
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
		//ResetHealth();
		StartCoroutine(MidRoundDelay());
	}

	private void EndAnimation()
	{
		//Put end animation here
	}
}
