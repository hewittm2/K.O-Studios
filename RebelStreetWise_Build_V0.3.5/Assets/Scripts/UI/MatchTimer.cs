using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour {


    public int minTime;
    public int secTime;

    public Text minText;
    public Text secText;

    public Image winnerImage;
    public Sprite team1Wins;
    public Sprite team2Wins;

    private FighterClass[] fighterClass;

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
        if(secTime < 0){
            minTime -= 1;
            secTime = 59;
            if(minTime < 0){
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

        StartCoroutine(matchTimer());
    }
    void RoundEnd ()
    {
        /*
        FighterClass[] fighterClass = FindObjectsOfType<FighterClass>();

        int lowesthealth = GetComponent<FighterClass>().currentHealth;
        string loser = Mathf.Min(lowesthealth).ToString();
        print(loser);

        /*
        foreach (FighterClass fighter in fighterClass)
        {
            int lowesthealth = fighter.GetComponent<FighterClass>().currentHealth;
            string losername = Mathf.Min(lowesthealth.CompareTo(lowesthealth)).ToString();
            print(losername);
        }
        */
    }
}
