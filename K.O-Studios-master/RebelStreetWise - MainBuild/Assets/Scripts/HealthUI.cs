using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public Slider player1Slider;
    public FighterClass fighterClass1;

    public Slider player2Slider;
    public FighterClass fighterClass2;

    public Slider player3Slider;
    public FighterClass fighterClass3;

    public Slider player4Slider;
    public FighterClass fighterClass4;

    public Toggle team1Toggle1;
    public Toggle team1Toggle2;
    public Toggle team2Toggle1;
    public Toggle team2Toggle2;

    // Use this for initialization
    void Start () {
        fighterClass1.currentHealth = fighterClass1.totalHealth;
        fighterClass2.currentHealth = fighterClass2.totalHealth;
        fighterClass3.currentHealth = fighterClass3.totalHealth;
        fighterClass4.currentHealth = fighterClass4.totalHealth;
        team1Toggle1.isOn = false;
        team1Toggle2.isOn = false;
        team2Toggle1.isOn = false;
        team2Toggle2.isOn = false;
    }
	
	public void TakeDamage (int ammount)
    {
        player1Slider.value = fighterClass1.currentHealth;
        if (fighterClass1.currentHealth <= 0 || fighterClass2.currentHealth <= 0)
        {
            Team2Win();
            fighterClass1.currentHealth = 100;
            fighterClass2.currentHealth = 100;
            fighterClass3.currentHealth = 100;
            fighterClass4.currentHealth = 100;
        }
        if (fighterClass3.currentHealth <= 0 || fighterClass4.currentHealth <= 0)
        {
            Team1Win();
            fighterClass1.currentHealth = 100;
            fighterClass2.currentHealth = 100;
            fighterClass3.currentHealth = 100;
            fighterClass4.currentHealth = 100;
        }
    }
    public void Update()
    {
        player1Slider.value = fighterClass1.currentHealth;
        player2Slider.value = fighterClass2.currentHealth;
        player3Slider.value = fighterClass3.currentHealth;
        player4Slider.value = fighterClass4.currentHealth;


        // Ignore For actual Build for Testing only
        if (Input.GetKeyDown("1"))
        {
            fighterClass1.currentHealth -= 10;
        }
        if (Input.GetKeyDown("2"))
        {
            fighterClass2.currentHealth -= 10;
        }
        if (Input.GetKeyDown("3"))
        {
            fighterClass3.currentHealth -= 10;
        }
        if (Input.GetKeyDown("4"))
        {
            fighterClass4.currentHealth -= 10;
        }
        if (fighterClass1.currentHealth <= 0 || fighterClass2.currentHealth <= 0)
        {
            Team2Win();
            fighterClass1.currentHealth = 100;
            fighterClass2.currentHealth = 100;
            fighterClass3.currentHealth = 100;
            fighterClass4.currentHealth = 100;
        }
        if (fighterClass3.currentHealth <= 0 || fighterClass4.currentHealth <= 0)
        {
            Team1Win();
            fighterClass1.currentHealth = 100;
            fighterClass2.currentHealth = 100;
            fighterClass3.currentHealth = 100;
            fighterClass4.currentHealth = 100;
        }
    }
    public void Team1Win()
    {
        if (!team1Toggle1.isOn)
        {
            team1Toggle1.isOn = true;
        }
        else if(team1Toggle1.isOn)
        {
            team1Toggle2.isOn = true;
        }
        else if (team1Toggle1 && team1Toggle2)
        {
            //MatchEnd();
        }

    }
    public void Team2Win()
    {
        if (!team2Toggle1.isOn)
        {
            team2Toggle1.isOn = true;
        }
        else if (team2Toggle1.isOn)
        {
            team2Toggle2.isOn = true;
        }
        else if (team2Toggle1 && team2Toggle2)
        {
            //MatchEnd();
        }
    }
}
