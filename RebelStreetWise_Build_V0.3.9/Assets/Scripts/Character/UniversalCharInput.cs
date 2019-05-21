using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalCharInput : MonoBehaviour 
{
    //Players
    public bool PlayerOne = false;
    public bool PlayerTwo = false;
    public bool PlayerThree = false;
    public bool PlayerFour = false;

    //References
    private FighterClass cAssign;

    //Buttons
    string aButton;
    string bButton;
    string yButton;
    string xButton;
    string startButton;
    string backButton;
    string rightBumper;
    string leftBumper;
    string leftTrigger;
    string rightTrigger;
    string dpadY;
    string dpadX;
    string lStickY;
    string lStickX;
    string rStickY;
    string rStickX;

    void Start()
    {
        cAssign = GetComponent<FighterClass>();
		CheckPlayer ();
        //StartCoroutine(AssignDelay());
    }

    private IEnumerator AssignDelay()
    {
        yield return new WaitForSeconds(.5f);
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        //Checks which Player the Character belongs to
		if (PlayerOne) {
			SetControls (1);
			cAssign.playerNumber = 1;
			cAssign.teamNumber = 1;
		} else if (PlayerTwo) {
			SetControls (2);
			cAssign.playerNumber = 2;
			cAssign.teamNumber = 1;
		} else if (PlayerThree) {
			SetControls (3);
			cAssign.playerNumber = 3;
			cAssign.teamNumber = 2;
		} else if (PlayerFour) {
			SetControls(4);
			cAssign.playerNumber = 4;
			cAssign.teamNumber = 2;
		}
            
    }

    private void SetControls(int playerNum)
    {
        string player = playerNum.ToString();
        aButton = "A_" + player;
        bButton = "B_" + player;
        yButton = "Y_" + player;
        xButton = "X_" + player;
        startButton = "Start_" + player;
        backButton = "Back_" + player;
        rightBumper = "RBump_" + player;
        leftBumper = "LBump_" + player;
        leftTrigger = "TriggersL_" + player;
        rightTrigger = "TriggersR_" + player;
        dpadY = "DPad_YAxis_" + player;
        dpadX = "DPad_XAxis_" + player;
        lStickY = "L_YAxis_" + player;
        lStickX = "L_XAxis_" + player;
        rStickY = "R_YAxis_" + player;
        rStickX = "R_XAxis_" + player;
        CAssignment();
    }

    void CAssignment()
    {
		cAssign.controllerVariables.lightInput = aButton;
		cAssign.controllerVariables.medInput = xButton;
		cAssign.controllerVariables.heavyInput = yButton;
		cAssign.controllerVariables.specialInput = bButton;
		cAssign.controllerVariables.lockOnInput = leftBumper;
		cAssign.controllerVariables.teamInput = rightBumper;
		cAssign.controllerVariables.horiInput = lStickX;
		cAssign.controllerVariables.vertInput = lStickY;
		cAssign.controllerVariables.startButton = startButton;
		cAssign.controllerVariables.throwInput = leftTrigger;
    }
}
