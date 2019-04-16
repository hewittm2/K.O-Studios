//Created by Tyler
//Mitch updated
//Torrel Update
//4/1/2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public FighterClass[] fighters;
    public List<FighterClass> team1 = new List<FighterClass>();
    public List<FighterClass> team2 = new List<FighterClass>();

    public Slider sl1;
    public Slider sl2;
    public Slider sl3;
    public Slider sl4;

    void Start ()
    {
        fighters = FindObjectsOfType<FighterClass>();

        foreach (FighterClass fighter in fighters)
        {
            if (fighter.teamNumber == 1)
            {
                team1.Add(fighter);
            }
            else
                team2.Add(fighter);
        }
        sl1.maxValue = team1[0].totalHealth;
        sl2.maxValue = team1[1].totalHealth;
        sl3.maxValue = team2[0].totalHealth;
        sl4.maxValue = team2[1].totalHealth;
    }
    private void Update()
    {
        sl1.value = team1[0].currentHealth;
        sl2.value = team1[1].currentHealth;
        sl3.value = team2[0].currentHealth;
        sl4.value = team2[1].currentHealth;
    }
}
