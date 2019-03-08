﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ryan Van Dusen
// 3/5/19

//Inhertaing from FIGHTCLASS
public class Chracter2Specials : FighterClass {


    public ParticleSystem empoweredParticle;
    public float damageBuff = 1.1f;
    public int maxEmpowerTime;
    private float currentEmpowerTime = 0;

    public GameObject thrownObject;
    private GameObject clone;
    private Vector3 spawnPosition;

    public float throwSpeed;
    public float totalThrowTime;
    public float rotationSpeed;

    private Vector3 rotation;
    private float throwTime;
    private bool throwing = false;
    private bool throwSetUp = false;



	// Use this for initialization
	void Start () {


        damage = 5;

	}
	
	// Update is called once per frame
	void Update () {
        if(throwing == false)
        {
           
        }

        //player pushes button
        if (Input.GetButtonDown("B_1") && throwing == false)
        {
        
            //spawns a clone of the thrownobject gameobject
            clone = Instantiate(thrownObject);
            //sets clone position to orginal position
            clone.transform.position = thrownObject.transform.position;
            //sets the orginal gameobject to not active (so it appears the object is thrown)
            thrownObject.SetActive(false);
            //sets clone's own scale and angles since the orginal is parented to the model
            clone.transform.localScale = new Vector3(0.5f, 5, 0.5f);
            clone.transform.eulerAngles = new Vector3(0, 0, 0);
                
            throwing = true;
            
        }
        if (throwing == true)
        {
           
            //rotates clone
            clone.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            //starts a counter
            throwTime = throwTime + Time.deltaTime;
            //moves clone
            clone.transform.position += (transform.forward *throwSpeed) * Time.deltaTime ;
            
            //once the counter is >= to the time set in the inspecter the clone destroys and the orginal object is set back to active
            if(throwTime >= totalThrowTime)
            {
                
                throwing = false;
                throwSetUp = false;
                throwTime = 0;
                Destroy(clone);
                thrownObject.SetActive(true);
                   
            }
        }
    }

    void NeutralSpecial()
    {
        //player presses button
        if (Input.GetButtonDown("B_1"))
        {
            //checks to see if impower or not
            if (currentEmpowerTime == 0)
            {
                //buffs damage (value set in inspector)
                damage = damage * damageBuff;
                //starts a timer
                currentEmpowerTime = currentEmpowerTime + Time.deltaTime;
                //particles
                empoweredParticle.gameObject.SetActive(true);
            }
        }
        //once timer is as big as value set in inspector
        if (currentEmpowerTime >= maxEmpowerTime)
        {
            //takes away damage buff
            damage = damage / damageBuff;
            //particles end
            empoweredParticle.gameObject.SetActive(false);
            //resets timer
            currentEmpowerTime = 0;
        }
    }
    void BackSpecial()
    {

    }

}
