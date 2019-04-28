﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ryan Van Dusen, Chris B.
//3/29/19
public class Grabs : MonoBehaviour
{

    public float grabDelay;
    public float knockBackTime;
    private float timeCounter;
    public float range;
    public float kickDistance;
    public GameObject enemy;
    Vector3 playerPosition;
    Vector3 enemyPosition;

    private bool startCount;
    private bool foward;
    private bool backward;

    public Animator anim;

    public int fowardDamage;
    public int backwardDamage;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;

        //Flipped this
        //enemyPosition = gameObject.GetComponent<FighterClass>().lockOnTarget.transform.position;
        //enemy = gameObject.GetComponent<FighterClass>().lockOnTarget;

        enemy = gameObject.GetComponent<FighterClass>().lockOnTarget;
        if(enemy != null)
        {
            enemyPosition = enemy.transform.position;
        }

        //foward grab
        //start a delay counter
        if (Input.GetButtonDown("A_1") && foward == false)
        {
            startCount = true;
            foward = true;

        }

        //backward grab
        //starts a delay counter
        if (Input.GetButtonDown("B_1") && backward == false)
        {
            startCount = true;
            backward = true;

        }
        //counter for delay
        if (startCount == true)
        {

            timeCounter += Time.deltaTime;
        }

        if (timeCounter >= grabDelay)
        {
            //foward grab
            if (foward == true)
            {
                if (GetComponent<FighterClass>().facingRight == true)
                {
                    if (playerPosition.x + range > enemyPosition.x)
                    {
                        anim.SetTrigger("FowardGrab");
                        enemy.transform.Translate(Vector3.back * kickDistance);
                        startCount = false;
                        foward = false;
                        timeCounter = 0;
                        //deal damage
                    }
                }
                if (GetComponent<FighterClass>().facingRight == false)
                {
                    if (playerPosition.x - range < enemyPosition.x)
                    {
                        anim.SetTrigger("FowardGrab");
                        enemy.transform.Translate(Vector3.back * kickDistance);
                        startCount = false;
                        foward = false;
                        timeCounter = 0;
                        //deal damage
                    }
                }
            }
            //backward grab
            if (backward == true)
            {
                if (GetComponent<FighterClass>().facingRight == true)
                {
                    if (playerPosition.x + range > enemyPosition.x)
                    {
                        anim.SetTrigger("BackwardGrab");
                        enemy.transform.Translate(new Vector3(0, 0, 10));
                        startCount = false;
                        backward = false;
                        timeCounter = 0;
                        //deal damage
                    }
                }
                if (GetComponent<FighterClass>().facingRight == false)
                {
                    if (playerPosition.x - range < enemyPosition.x)
                    {
                        anim.SetTrigger("BackwardGrab");
                        enemy.transform.Translate(new Vector3(0, 0, 10));
                        startCount = false;
                        backward = false;
                        timeCounter = 0;
                        //deal damage
                    }
                }
            }
        }

    }


}
