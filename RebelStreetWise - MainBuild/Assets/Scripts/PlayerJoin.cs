// Created by Ryan Van Dusen Torrel LLoyd 10/03/18
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJoin : MonoBehaviour
{   
    //Checks if player controller can input or not
    public bool player1Active = true;
    public bool player2Active = false;
    public bool player3Active = false;
    public bool player4Active = false;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Runs the function
        PlayerAdd();
    }
    void PlayerAdd()
    {
   
        #region Set Player Active
        //allows controller inputs
        if (Input.GetButtonDown("Start_1"))
        {
      
            player1Active = true;
            Debug.Log("Player1 Active");
        }
        if (Input.GetButtonDown("Start_2"))
        {

            player2Active = true;
            Debug.Log("Player2 Active");
        }
        if (Input.GetButtonDown("Start_3"))
        {

            player3Active = true;
            Debug.Log("Player3 Active");
        }
        if (Input.GetButtonDown("Start_4"))
        {

            player4Active = true;
            Debug.Log("Player4 Active");
        }
        #endregion
        #region Set Player Inactive
        //stop controller inputs
        if (Input.GetButtonDown("Back_1"))
        {
            player1Active = false;
            Debug.Log("Player1 Inactive");
        }
        if (Input.GetButtonDown("Back_2"))
        {
            player2Active = false;
            Debug.Log("Player2 Inactive");
        }
        if (Input.GetButtonDown("Back_3"))
        {
            player3Active = false;
            Debug.Log("Player3 Inactive");
        }
        if (Input.GetButtonDown("Back_4"))
        {
            player4Active = false;
            Debug.Log("Player4 Inactive");
        }
        #endregion

    }
}
