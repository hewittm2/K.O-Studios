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
        StartCoroutine(AssignDelay());
    }

    private IEnumerator AssignDelay()
    {
        yield return new WaitForSeconds(.5f);
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        //Checks which Player the Character belongs to
        if (PlayerOne)
            SetControls(1);
        if (PlayerTwo)
            SetControls(2);
        if (PlayerThree)
            SetControls(3);
        if (PlayerFour)
            SetControls(4);
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
        cAssign.lightInput = aButton;
        cAssign.medInput = xButton;
        cAssign.heavyInput = yButton;
        cAssign.specialInput = bButton;
        cAssign.dashInput = leftBumper;
        cAssign.horiInput = lStickX;
        cAssign.vertInput = lStickY;
    }
}
