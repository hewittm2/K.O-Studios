using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMeter : MonoBehaviour {

    public Slider team1Meter;
    public int meterCharge1;
    public int maxCharge1 = 300;
    public int meterSpent = 75;

    public Slider team2Meter;
    public int meterCharge2;
    public int maxCharge2 = 300;

    public FighterClass fighterClass1;
    public FighterClass fighterClass2;
    public FighterClass fighterClass3;
    public FighterClass fighterClass4;

    public void GainCharge(int ammount, int PlayerNum)
    {
        if (PlayerNum ==  1 || PlayerNum == 2)
        {
            meterCharge1 += ammount;
        }
        if (PlayerNum == 3 || PlayerNum == 4)
        {
            meterCharge2 += ammount;
        }
    }
    private void Update()
    {
        team1Meter.value = meterCharge1;
        team2Meter.value = meterCharge2;

        if (meterCharge1 >= maxCharge1)
        {
            meterCharge1 = maxCharge1;
        }
        if (meterCharge2 >= maxCharge2)
        {
            meterCharge2 = maxCharge2;
        }

        // Ignore This is For Testing Only
        if (Input.GetKeyDown("5") && meterCharge1 >= meterSpent)
        {
            meterCharge1 -= meterSpent;
        }
        if (Input.GetKeyDown("6") && meterCharge2 >= meterSpent)
        {
            meterCharge2 -= meterSpent;
        }
        //if (Input.GetKeyDown("7"))
        //{
        //    meterCharge1 += meterSpent;
        //}
        if (Input.GetKeyDown("8"))
        {
            meterCharge2 += meterSpent;
        }
    }

    public void BreakDownCharge()
    {
        if (fighterClass1 || fighterClass2)
        {
            meterCharge1 -= meterSpent;
        }
        if (fighterClass3 || fighterClass4)
        {
            meterCharge2 -= meterSpent;
        }
    }
}
