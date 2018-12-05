/*
 * #KO Studios#
 * #Rebel Streetwise#
 *
 * #MatchTimer#.cs
 *
 * Purpose: Control the match timer and canvas's with round over splash
 * Usage:
 * Notes:
 *
 * Author: Cale Toburen
 * Creation Date: #10-31-18#
 *
*/

#region CHANGELOG

/*********************************************************************************/
/*
 * Date:
 * Editor:
 * Changes:
 *
/*********************************************************************************/

#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0414

public class MatchTimer : MonoBehaviour {

    #region(vars)

    [SerializeField]
    private float matchTimer;
    [SerializeField]
    private float startTimer = 3;
    [SerializeField]
    private float startSplashTime;
    [SerializeField]
    private float endSplashTime;
    [SerializeField]
    private float matchLength;
    [SerializeField]
    private float roundDelay;
    [SerializeField]
    private int rounds;
    [SerializeField]
    private Text matchTimerText;
    [SerializeField]
    private GameObject roundOver;
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private GameObject roundStart;
    [SerializeField]
    private bool start;
    [SerializeField]
    private bool startMatch;
    [SerializeField]
    private bool endActive;
    [SerializeField]
    private Text timer;

    #endregion

    //disables text fields to reduce cluter
    void Awake()
    {
        matchTimer = matchLength;
        start = false;
        startMatch = false;
        endActive = false;
        matchTimerText.enabled = false;
        roundOver.SetActive(false);
        roundStart.SetActive(false);
        timer.enabled = false;
    }
    
    void Update()
    {
        if(rounds == 0 && startMatch)
        {
            StartMatch();
        }
        if(rounds <= 3 && start)
        {
            MatchTime();
        }
        roundText.text = rounds.ToString();
    }

    //match start timer
    void StartMatch()
    {
        timer.enabled = true;
        if (startTimer < 0)
        {
            StartCoroutine(StartRoundDisplay());
            timer.enabled = false;
            StartCoroutine(RoundInc());
        }
        if(startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            int tempStart = Mathf.RoundToInt(startTimer);
            timer.text = tempStart.ToString();

        }
    }

    //main match timer
    void MatchTime()
    {
        timer.enabled = false;
        matchTimerText.enabled = true;
        if (matchTimer > 0)
        {
            matchTimer -= Time.deltaTime;
            int tempStart = Mathf.RoundToInt(matchTimer);
            matchTimerText.text = tempStart.ToString();
        }
        if(matchTimer <= 0)
        {
            StartCoroutine(EndRoundDisplay());
            matchTimerText.enabled = false;
        }
    }

    IEnumerator StartRoundDisplay()
    {
        roundStart.SetActive(true);
        yield return new WaitForSeconds(startSplashTime);
        roundStart.SetActive(false);
    }

    IEnumerator EndRoundDisplay()
    {
        //look to health total to give score
        endActive = true;
        roundOver.SetActive(true);
        yield return new WaitForSeconds(endSplashTime);
        roundOver.SetActive(false);
        if (rounds < 3)
        {
            StartMatch();
        }
    }

    IEnumerator RoundInc()
    {
        start = false;
        rounds += 1;
        matchTimer = matchLength;
        startTimer = 3;
        start = true;
        yield return null;
    }
}
