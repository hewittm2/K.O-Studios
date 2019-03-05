using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
//
//    //LookAtVars
//    public int player1LookAt = 0;
//    public int player2LookAt = 1;
//    public int player3LookAt = 0;
//    public int player4LookAt = 1;
//    private Vector3 targetPosition;
//    private Vector3 targetPosition2;
//    private Vector3 targetPosition3;
//    private Vector3 targetPosition4;
//    bool player1Changed = false;
//    bool player2Changed = false;
//    bool player3Changed = false;
//    bool player4Changed = false;
//    public GameManagerAlpha GameManager;
//    public FighterClass buttonGetter;
//
//    // Use this for initialization
//    void Start () {
//        GameManager.team1[0].transform.LookAt(GameManager.team2[0].transform);
//        GameManager.team2[0].transform.LookAt(GameManager.team1[0].transform);
//        GameManager.team1[1].transform.LookAt(GameManager.team2[1].transform);
//        GameManager.team2[1].transform.LookAt(GameManager.team1[1].transform);
//    }
//	
//	// Update is called once per frame
//	void Update () {
//        LookChange();	
//	}
//    public void LookChange()
//    {
//        if (Input.GetButtonDown("LBump_1"))
//        {
//            Debug.Log("Player one is looking at " + player1LookAt);
//            player1Changed = true;
//            if (player1LookAt == 0)
//            {
//                player1LookAt = 1;
//            }
//            else
//                player1LookAt = 0;
//
//            LookChangeMovement();
//            player1Changed = false;
//            Debug.Log("Player one is looking at " + player1LookAt);
//        }
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            player2Changed = true;
//            if (player2LookAt == 0)
//            {
//                player2LookAt = 1;
//            }
//            else
//                player2LookAt = 0;
//
//            LookChangeMovement();
//            player2Changed = false;
//        }
//
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            player3Changed = true;
//            if (player3LookAt == 0)
//            {
//                player3LookAt = 1;
//            }
//            else
//                player3LookAt = 0;
//
//            LookChangeMovement();
//            player3Changed = false;
//        }
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            player4Changed = true;
//            if (player4LookAt == 0)
//            {
//                player4LookAt = 1;
//            }
//            else
//                player4LookAt = 0;
//
//            LookChangeMovement();
//            player4Changed = false;
//        }
//    }
//    public void LookChangeMovement()
//    {
//        if (player1Changed == true)
//        {
//
//            //gets looked at targets x and z position but maintains characters own y position.
//            targetPosition = new Vector3(GameManager.team2[player1LookAt].transform.position.x, GameManager.team1[0].transform.position.y, GameManager.team2[player1LookAt].transform.position.z);
//            //dont get it but for some reason it only moves on the y axis
//            GameManager.team1[0].transform.LookAt(targetPosition);
//        }
//        if (player2Changed == true)
//        {
//
//            //gets looked at targets x and z position but maintains characters own y position.
//            targetPosition2 = new Vector3(GameManager.team2[player2LookAt].transform.position.x, GameManager.team1[1].transform.position.y, GameManager.team2[player2LookAt].transform.position.z);
//            //dont get it but for some reason it only moves on the y axis
//            GameManager.team1[1].transform.LookAt(targetPosition);
//        }
//        if (player3Changed == true)
//        {
//
//            //gets looked at targets x and z position but maintains characters own y position.
//            targetPosition3 = new Vector3(GameManager.team1[player3LookAt].transform.position.x, GameManager.team2[0].transform.position.y, GameManager.team1[player3LookAt].transform.position.z);
//            //dont get it but for some reason it only moves on the y axis
//            GameManager.team2[0].transform.LookAt(targetPosition);
//        }
//        if (player4Changed == true)
//        {
//
//            //gets looked at targets x and z position but maintains characters own y position.
//            targetPosition4 = new Vector3(GameManager.team1[player4LookAt].transform.position.x, GameManager.team2[1].transform.position.y, GameManager.team1[player4LookAt].transform.position.z);
//            //dont get it but for some reason it only moves on the y axis
//            GameManager.team2[1].transform.LookAt(targetPosition);
//        }
//
//
//
//
//
//
//
//
//
//
//




        //Quaternion.RotateTowards(GameManager.team1[0].transform.rotation, GameManager.team2[player1LookAt].transform.rotation, 180);

        //Quaternion.LookRotation(GameManager.team2[player1LookAt].transform.position);
        //if(GameManager.team1[0].transform.position.x < GameManager.team2[player1LookAt].transform.position.x)
        //{
        //    Vector3.RotateTowards(GameManager.team1[0].transform.position, GameManager.team2[player1LookAt].transform.position, 180,180);
        //}
        //else if (GameManager.team1[0].transform.position.x > GameManager.team2[player1LookAt].transform.position.x)
        //{
        //    Vector3.RotateTowards(GameManager.team1[0].transform.position, GameManager.team2[player1LookAt].transform.position, 180, 180);
        //}

        //if (player2Changed == true)
        //{
        //    targetPosition2 = new Vector3(GameManager.team2[player2LookAt].transform.position.x, transform.position.y, GameManager.team2[player2LookAt].transform.position.z);
        //    GameManager.team1[1].transform.LookAt(targetPosition2);
        //}
        //if (player1Changed == true)
        //{
        //    targetPosition3 = new Vector3(GameManager.team1[player3LookAt].transform.position.x, transform.position.y, GameManager.team1[player3LookAt].transform.position.z);
        //    GameManager.team2[0].transform.LookAt(targetPosition3);
        //}
        //if (player1Changed == true)
        //{
        //    targetPosition4 = new Vector3(GameManager.team1[player4LookAt].transform.position.x, transform.position.y, GameManager.team1[player4LookAt].transform.position.z);
        //    GameManager.team2[1].transform.LookAt(targetPosition4);
        //}


    //}
}
