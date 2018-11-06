// Created by Ryan Van Dusen Torrel LLoyd 10/03/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputs : MonoBehaviour {
    //drop whatever Playerjoin is attached to in this slot
    public GameObject GameManger;
    //debug storage
    string controller1Name;
    string controller2Name;
    string controller3Name;
    string controller4Name;

    // Use this for initialization
    void Start () {
        
        string[] temp = Input.GetJoystickNames();
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; i++)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                    if(i == 0)
                    controller1Name = temp[0];
                    if (i == 1)
                        controller2Name = temp[1];
                    if (i == 2)
                        controller3Name = temp[2];
                    if (i == 3)
                        controller4Name = temp[3];
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");
                
                
                }
            }
        }

    }

    // Update is called once per frame
    void Update () {
        //Buttons
        #region Player One
        if (GameManger.gameObject.GetComponent<PlayerJoin>().player1Active == true)
        {
            if (Input.GetButtonDown("A_1"))
            {
                Debug.Log("Controller 1: A Pressed");
            }
            if (Input.GetButtonDown("B_1"))
            {
                Debug.Log("Controller 1: B Pressed");
            }
            if (Input.GetButtonDown("Y_1"))
            {
                Debug.Log("Controller 1: Y Pressed");
            }
            if (Input.GetButtonDown("X_1"))
            {
                Debug.Log("Controller 1: X Pressed");
            }
            if (Input.GetButtonDown("RBump_1"))
            {
                Debug.Log("Controller 1: RBump Pressed");
            }
            if (Input.GetButtonDown("LBump_1"))
            {
                Debug.Log("Controller 1: LBump Pressed");
            }
            if (Input.GetButtonDown("Start_1"))
            {
                Debug.Log("Controller 1: Start Pressed");
            }
            if (Input.GetButtonDown("Back_1"))
            {
                Debug.Log("Controller 1: Back Pressed");
            }

            //JoySticks
            if (Input.GetAxisRaw("L_XAxis_1") > 0)
            {
                Debug.Log("Controller 1: LSX+Value: " + Input.GetAxisRaw("L_XAxis_1"));

            }
            if (Input.GetAxisRaw("L_XAxis_1") < 0)
            {
                Debug.Log("Controller 1: LSX-Value: " + Input.GetAxisRaw("L_XAxis_1"));

            }
            if (Input.GetAxisRaw("L_YAxis_1") < 0)
            {
                Debug.Log("Controller 1: LSY+Value: " + Input.GetAxisRaw("L_YAxis_1"));
            }
            if (Input.GetAxisRaw("L_YAxis_1") > 0)
            {
                Debug.Log("Controller 1: LSY-Value: " + Input.GetAxisRaw("L_YAxis_1"));
            }
            if (Input.GetAxisRaw("R_XAxis_1") > 0)
            {
                Debug.Log("Controller 1: RSX+Value: " + Input.GetAxisRaw("R_XAxis_1"));
            }
            if (Input.GetAxisRaw("R_XAxis_1") < 0)
            {
                Debug.Log("Controller 1: RSX-Value: " + Input.GetAxisRaw("R_XAxis_1"));
            }
            if (Input.GetAxisRaw("R_YAxis_1") < 0)
            {
                Debug.Log("Controller 1: RSY+Value: " + Input.GetAxisRaw("R_YAxis_1"));
            }
            if (Input.GetAxisRaw("R_YAxis_1") > 0)
            {
                Debug.Log("Controller 1: RSY-Value: " + Input.GetAxisRaw("R_YAxis_1"));
            }
            //Triggers
            if (Input.GetAxisRaw("TriggersL_1") > 0)
            {
                Debug.Log("Controller 1: LeftValue: " + Input.GetAxisRaw("TriggersL_1"));
            }
            if (Input.GetAxisRaw("TriggersR_1") > 0)
            {
                Debug.Log("Controller 1: RightValue: " + Input.GetAxisRaw("TriggersR_1"));
            }
            //D-Pad
            if (Input.GetAxisRaw("DPad_XAxis_1") > 0)
            {
                Debug.Log("Controller 1:X+Value: " + Input.GetAxisRaw("DPad_XAxis_1"));
            }
            if (Input.GetAxisRaw("DPad_XAxis_1") < 0)
            {
                Debug.Log("Controller 1:X-Value: " + Input.GetAxisRaw("DPad_XAxis_1"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_1") > 0)
            {
                Debug.Log("Controller 1:Y+Value: " + Input.GetAxisRaw("DPad_YAxis_1"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_1") < 0)
            {
                Debug.Log("Controller 1: Y-Value: " + Input.GetAxisRaw("DPad_YAxis_1"));
            }
        }
        #endregion
        #region Player Two
        if (GameManger.gameObject.GetComponent<PlayerJoin>().player2Active == true)
        {
            if (Input.GetButtonDown("A_2"))
            {
                Debug.Log("Controller 2: A Pressed");
            }
            if (Input.GetButtonDown("B_2"))
            {
                Debug.Log("Controller 2: B Pressed");
            }
            if (Input.GetButtonDown("Y_2"))
            {
                Debug.Log("Controller 2: Y Pressed");
            }
            if (Input.GetButtonDown("X_2"))
            {
                Debug.Log("Controller 2: X Pressed");
            }
            if (Input.GetButtonDown("RBump_2"))
            {
                Debug.Log("Controller 2: RBump Pressed");
            }
            if (Input.GetButtonDown("LBump_2"))
            {
                Debug.Log("Controller 2: LBump Pressed");
            }
            if (Input.GetButtonDown("Start_2"))
            {
                Debug.Log("Controller 2: Start Pressed");
            }
            if (Input.GetButtonDown("Back_2"))
            {
                Debug.Log("Controller 2: Back Pressed");
            }

            //JoySticks
            if (Input.GetAxisRaw("L_XAxis_2") > 0)
            {
                Debug.Log("Controller 2: LSX+Value: " + Input.GetAxisRaw("L_XAxis_2"));

            }
            if (Input.GetAxisRaw("L_XAxis_2") < 0)
            {
                Debug.Log("Controller 2: LSX-Value: " + Input.GetAxisRaw("L_XAxis_2"));

            }
            if (Input.GetAxisRaw("L_YAxis_2") < 0)
            {
                Debug.Log("Controller 2: LSY+Value: " + Input.GetAxisRaw("L_YAxis_2"));
            }
            if (Input.GetAxisRaw("L_YAxis_2") > 0)
            {
                Debug.Log("Controller 2: LSY-Value: " + Input.GetAxisRaw("L_YAxis_2"));
            }
            if (Input.GetAxisRaw("R_XAxis_2") > 0)
            {
                Debug.Log("Controller 2: RSX+Value: " + Input.GetAxisRaw("R_XAxis_2"));
            }
            if (Input.GetAxisRaw("R_XAxis_2") < 0)
            {
                Debug.Log("Controller 2: RSX-Value: " + Input.GetAxisRaw("R_XAxis_2"));
            }
            if (Input.GetAxisRaw("R_YAxis_2") < 0)
            {
                Debug.Log("Controller 2: RSY+Value: " + Input.GetAxisRaw("R_YAxis_2"));
            }
            if (Input.GetAxisRaw("R_YAxis_2") > 0)
            {
                Debug.Log("Controller 2: RSY-Value: " + Input.GetAxisRaw("R_YAxis_2"));
            }
            //Triggers
            if (Input.GetAxisRaw("TriggersL_2") > 0)
            {
                Debug.Log("Controller 2: LeftValue: " + Input.GetAxisRaw("TriggersL_2"));
            }
            if (Input.GetAxisRaw("TriggersR_2") > 0)
            {
                Debug.Log("Controller 2: RightValue: " + Input.GetAxisRaw("TriggersR_2"));
            }
            //D-Pad
            if (Input.GetAxisRaw("DPad_XAxis_2") > 0)
            {
                Debug.Log("Controller 2:X+Value: " + Input.GetAxisRaw("DPad_XAxis_2"));
            }
            if (Input.GetAxisRaw("DPad_XAxis_2") < 0)
            {
                Debug.Log("Controller 2:X-Value: " + Input.GetAxisRaw("DPad_XAxis_2"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_2") > 0)
            {
                Debug.Log("Controller 2:Y+Value: " + Input.GetAxisRaw("DPad_YAxis_2"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_2") < 0)
            {
                Debug.Log("Controller 2: Y-Value: " + Input.GetAxisRaw("DPad_YAxis_2"));
            }
        }
        #endregion
        #region Player Three
        if (GameManger.gameObject.GetComponent<PlayerJoin>().player3Active == true)
        {
            if (Input.GetButtonDown("A_3"))
            {
                Debug.Log("Controller 3: A Pressed");
            }
            if (Input.GetButtonDown("B_3"))
            {
                Debug.Log("Controller 3: B Pressed");
            }
            if (Input.GetButtonDown("Y_3"))
            {
                Debug.Log("Controller 3: Y Pressed");
            }
            if (Input.GetButtonDown("X_3"))
            {
                Debug.Log("Controller 3: X Pressed");
            }
            if (Input.GetButtonDown("RBump_3"))
            {
                Debug.Log("Controller 3: RBump Pressed");
            }
            if (Input.GetButtonDown("LBump_3"))
            {
                Debug.Log("Controller 3: LBump Pressed");
            }
            if (Input.GetButtonDown("Start_3"))
            {
                Debug.Log("Controller 3: Start Pressed");
            }
            if (Input.GetButtonDown("Back_3"))
            {
                Debug.Log("Controller 3: Back Pressed");
            }

            //JoySticks
            if (Input.GetAxisRaw("L_XAxis_3") > 0)
            {
                Debug.Log("Controller 3: LSX+Value: " + Input.GetAxisRaw("L_XAxis_3"));

            }
            if (Input.GetAxisRaw("L_XAxis_3") < 0)
            {
                Debug.Log("Controller 3: LSX-Value: " + Input.GetAxisRaw("L_XAxis_3"));

            }
            if (Input.GetAxisRaw("L_YAxis_3") < 0)
            {
                Debug.Log("Controller 3: LSY+Value: " + Input.GetAxisRaw("L_YAxis_3"));
            }
            if (Input.GetAxisRaw("L_YAxis_3") > 0)
            {
                Debug.Log("Controller 3: LSY-Value: " + Input.GetAxisRaw("L_YAxis_3"));
            }
            if (Input.GetAxisRaw("R_XAxis_3") > 0)
            {
                Debug.Log("Controller 3: RSX+Value: " + Input.GetAxisRaw("R_XAxis_3"));
            }
            if (Input.GetAxisRaw("R_XAxis_3") < 0)
            {
                Debug.Log("Controller 3: RSX-Value: " + Input.GetAxisRaw("R_XAxis_3"));
            }
            if (Input.GetAxisRaw("R_YAxis_3") < 0)
            {
                Debug.Log("Controller 3: RSY+Value: " + Input.GetAxisRaw("R_YAxis_3"));
            }
            if (Input.GetAxisRaw("R_YAxis_3") > 0)
            {
                Debug.Log("Controller 3: RSY-Value: " + Input.GetAxisRaw("R_YAxis_3"));
            }
            //Triggers
            if (Input.GetAxisRaw("TriggersL_3") > 0)
            {
                Debug.Log("Controller 3: LeftValue: " + Input.GetAxisRaw("TriggersL_3"));
            }
            if (Input.GetAxisRaw("TriggersR_3") > 0)
            {
                Debug.Log("Controller 3: RightValue: " + Input.GetAxisRaw("TriggersR_3"));
            }
            //D-Pad
            if (Input.GetAxisRaw("DPad_XAxis_3") > 0)
            {
                Debug.Log("Controller 3:X+Value: " + Input.GetAxisRaw("DPad_XAxis_3"));
            }
            if (Input.GetAxisRaw("DPad_XAxis_3") < 0)
            {
                Debug.Log("Controller 3:X-Value: " + Input.GetAxisRaw("DPad_XAxis_3"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_3") > 0)
            {
                Debug.Log("Controller 3:Y+Value: " + Input.GetAxisRaw("DPad_YAxis_3"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_3") < 0)
            {
                Debug.Log("Controller 3: Y-Value: " + Input.GetAxisRaw("DPad_YAxis_3"));
            }
        }
        #endregion
        #region Player Four
        if (GameManger.gameObject.GetComponent<PlayerJoin>().player4Active == true)
        {
            if (Input.GetButtonDown("A_4"))
            {
                Debug.Log("Controller 4: A Pressed");
            }
            if (Input.GetButtonDown("B_4"))
            {
                Debug.Log("Controller 4: B Pressed");
            }
            if (Input.GetButtonDown("Y_4"))
            {
                Debug.Log("Controller 4: Y Pressed");
            }
            if (Input.GetButtonDown("X_4"))
            {
                Debug.Log("Controller 4: X Pressed");
            }
            if (Input.GetButtonDown("RBump_4"))
            {
                Debug.Log("Controller 4: RBump Pressed");
            }
            if (Input.GetButtonDown("LBump_4"))
            {
                Debug.Log("Controller 4: LBump Pressed");
            }
            if (Input.GetButtonDown("Start_4"))
            {
                Debug.Log("Controller 4: Start Pressed");
            }
            if (Input.GetButtonDown("Back_4"))
            {
                Debug.Log("Controller 4: Back Pressed");
            }

            //JoySticks
            if (Input.GetAxisRaw("L_XAxis_4") > 0)
            {
                Debug.Log("Controller 4: LSX+Value: " + Input.GetAxisRaw("L_XAxis_4"));

            }
            if (Input.GetAxisRaw("L_XAxis_4") < 0)
            {
                Debug.Log("Controller 4: LSX-Value: " + Input.GetAxisRaw("L_XAxis_4"));

            }
            if (Input.GetAxisRaw("L_YAxis_4") < 0)
            {
                Debug.Log("Controller 4: LSY+Value: " + Input.GetAxisRaw("L_YAxis_4"));
            }
            if (Input.GetAxisRaw("L_YAxis_4") > 0)
            {
                Debug.Log("Controller 4: LSY-Value: " + Input.GetAxisRaw("L_YAxis_4"));
            }
            if (Input.GetAxisRaw("R_XAxis_4") > 0)
            {
                Debug.Log("Controller 4: RSX+Value: " + Input.GetAxisRaw("R_XAxis_4"));
            }
            if (Input.GetAxisRaw("R_XAxis_4") < 0)
            {
                Debug.Log("Controller 4: RSX-Value: " + Input.GetAxisRaw("R_XAxis_4"));
            }
            if (Input.GetAxisRaw("R_YAxis_4") < 0)
            {
                Debug.Log("Controller 4: RSY+Value: " + Input.GetAxisRaw("R_YAxis_4"));
            }
            if (Input.GetAxisRaw("R_YAxis_4") > 0)
            {
                Debug.Log("Controller 4: RSY-Value: " + Input.GetAxisRaw("R_YAxis_4"));
            }
            //Triggers
            if (Input.GetAxisRaw("TriggersL_4") > 0)
            {
                Debug.Log("Controller 4: LeftValue: " + Input.GetAxisRaw("TriggersL_4"));
            }
            if (Input.GetAxisRaw("TriggersR_4") > 0)
            {
                Debug.Log("Controller 4: RightValue: " + Input.GetAxisRaw("TriggersR_4"));
            }
            //D-Pad
            if (Input.GetAxisRaw("DPad_XAxis_4") > 0)
            {
                Debug.Log("Controller 4:X+Value: " + Input.GetAxisRaw("DPad_XAxis_4"));
            }
            if (Input.GetAxisRaw("DPad_XAxis_4") < 0)
            {
                Debug.Log("Controller 4:X-Value: " + Input.GetAxisRaw("DPad_XAxis_4"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_4") > 0)
            {
                Debug.Log("Controller 4:Y+Value: " + Input.GetAxisRaw("DPad_YAxis_4"));
            }
            if (Input.GetAxisRaw("DPad_YAxis_4") < 0)
            {
                Debug.Log("Controller 4: Y-Value: " + Input.GetAxisRaw("DPad_YAxis_4"));
            }
        }
        #endregion








    }

}
