using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalCharInput : MonoBehaviour {

    //Players
    public bool PlayerOne = false;
    public bool PlayerTwo = false;
    public bool PlayerThree = false;
    public bool PlayerFour = false;
    //References
    private FighterClass cAssign;
    private CharacterSpawning spawnAssign;
    //buttons
    public string aButton;
    public string bButton;
    public string yButton;
    public string xButton;
    public string startButton;
    public string backButton;
    public string rightBumper;
    public string leftBumper;
    public string leftTrigger;
    public string rightTrigger;
    public string dpadY;
    public string dpadX;
    public string lStickY;
    public string lStickX;
    public string rStickY;
    public string rStickX;

    void Start()
    {
        cAssign = GetComponent<FighterClass>();

      


    }
    void Update()
    {
        //checks if player what player is active

        if (PlayerOne == true)
        {
            aButton = "A_1";
            bButton = "B_1";
            yButton = "Y_1";
            xButton = "X_1";
            startButton = "Start_1";
            backButton = "Back_1";
            rightBumper = "RBump_1";
            leftBumper = "LBump_1";
            leftTrigger = "TriggersL_1";
            rightTrigger= "TriggersR_1";
            dpadY = "DPad_YAxis_1";
            dpadX = "DPad_XAxis_1";
            lStickY = "L_XAxis_1";
            lStickX = "L_YAxis_1";
            rStickY = "R_YAxis_1";
            rStickX = "R_XAxis_1";
            CAssignment();
        }

        if (PlayerTwo == true)
        {
            aButton = "A_2";
            bButton = "B_2";
            yButton = "Y_2";
            xButton = "X_2";
            startButton = "Start_2";
            backButton = "Back_2";
            rightBumper = "RBump_2";
            leftBumper = "LBump_2";
            leftTrigger = "TriggersL_2";
            rightTrigger = "TriggersR_2";
            dpadY = "DPad_YAxis_2";
            dpadX = "DPad_XAxis_2";
            lStickY = "L_XAxis_2";
            lStickX = "L_YAxis_2";
            rStickY = "R_YAxis_2";
            rStickX = "R_XAxis_2";
            CAssignment();
        }
        if (PlayerThree == true)
        {
            aButton = "A_3";
            bButton = "B_3";
            yButton = "Y_3";
            xButton = "X_3";
            startButton = "Start_3";
            backButton = "Back_3";
            rightBumper = "RBump_3";
            leftBumper = "LBump_3";
            leftTrigger = "TriggersL_3";
            rightTrigger = "TriggersR_3";
            dpadY = "DPad_YAxis_3";
            dpadX = "DPad_XAxis_3";
            lStickY = "L_XAxis_3";
            lStickX = "L_YAxis_3";
            rStickY = "R_YAxis_3";
            rStickX = "R_XAxis_3";
            CAssignment();
        }
        if (PlayerFour == true)
        {
            aButton = "A_4";
            bButton = "B_4";
            yButton = "Y_4";
            xButton = "X_4";
            startButton = "Start_4";
            backButton = "Back_4";
            rightBumper = "RBump_4";
            leftBumper = "LBump_4";
            leftTrigger = "TriggersL_4";
            rightTrigger = "TriggersR_4";
            dpadY = "DPad_YAxis_4";
            dpadX = "DPad_XAxis_4";
            lStickY = "L_XAxis_4";
            lStickX = "L_YAxis_4";
            rStickY = "R_YAxis_4";
            rStickX = "R_XAxis_4";
            CAssignment();
        }
    }

    void CAssignment()
    {
        cAssign.lightInput = aButton;
        cAssign.medInput = xButton;
        cAssign.heavyInput = yButton;
        cAssign.specialInput = bButton;
        cAssign.dashInput = leftBumper;
        //cAssign.grabInput = rightBumper;
        cAssign.horiInput = lStickX;
        cAssign.vertInput = rStickX;
    }
}
