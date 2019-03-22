using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabs : MonoBehaviour {

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

 




	// Use this for initialization
	void Start () {
    
       
	}

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        enemyPosition = gameObject.GetComponent<FighterClass>().lockOnTarget.transform.position;
        enemy = gameObject.GetComponent<FighterClass>().lockOnTarget;



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
                        enemy.transform.Translate(Vector3.back * kickDistance);
                        startCount = false;
                        foward = false;
                        timeCounter = 0;
                    }
                }
                if (GetComponent<FighterClass>().facingRight == false)
                {
                    if (playerPosition.x - range < enemyPosition.x)
                    {
                        enemy.transform.Translate(Vector3.back * kickDistance);
                        startCount = false;
                        foward = false;
                        timeCounter = 0;
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

                        enemy.transform.Translate(new Vector3(0, 0, 10));
                        startCount = false;
                        backward = false;
                        timeCounter = 0;
                    }
                }
                if (GetComponent<FighterClass>().facingRight == false)
                {
                    if (playerPosition.x - range < enemyPosition.x)
                    {
                        enemy.transform.Translate(new Vector3(0, 0, 10));
                        startCount = false;
                        backward = false;
                        timeCounter = 0;
                    }
                }
            }
        }

    }


}
