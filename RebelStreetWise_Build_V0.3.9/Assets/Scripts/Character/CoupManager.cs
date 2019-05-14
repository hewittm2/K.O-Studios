using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoupManager : MonoBehaviour
{
    [Range(0,1200f)]
    public float t1SuperMeter;
    public Slider t1Slider;
    [Range(0, 1200f)]
    public float t2SuperMeter;
    public Slider t2Slider;

    public int t1 = 0;
    public int t2 = 0;

    public bool t1CoupSuccessful;
    public bool t2CoupSuccessful;

    public FighterClass[] playerEnergy;
    public List<FighterClass> t1s;
    public List<FighterClass> t2s;

    private bool isready = false;
    IEnumerator Start ()
    {
        yield return new WaitForSeconds(0.4f);
        isready = true;
        playerEnergy = FindObjectsOfType<FighterClass>();

        foreach (FighterClass cc in playerEnergy)
        {
            if (cc.teamNumber == 1)
                t1s.Add(cc);

            if (cc.teamNumber == 2)
                t2s.Add(cc);
        }


        t1Slider.maxValue = 1200;
        t2Slider.maxValue = 1200;

        isready = true;

    }
	

	void Update ()
    {
        if (isready == true)
        {
            if (t1 == 2)
                t1CoupSuccessful = true;
            else
                t1CoupSuccessful = false;

            if (t2 == 2)
                t2CoupSuccessful = true;
            else
                t2CoupSuccessful = false;
            t1SuperMeter = t1s[0].superMeter + t1s[1].superMeter;

            t2SuperMeter = t2s[0].superMeter + t2s[1].superMeter;



            t1Slider.value = t1SuperMeter;
            t2Slider.value = t2SuperMeter;
        }

    }
}
