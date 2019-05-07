﻿//Torrel L. Ethan Q.
//4/8/2019
//Projectile Damage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFighterReference : MonoBehaviour
{
    public bool passThrough = false;
    public FighterClass fighter;
	public FighterClass.AttackStats output;
	public bool hit;

    private void OnEnable(){
		hit = false;
        if (fighter == null)
            fighter = GetComponentInParent<FighterClass>();

		output = fighter.output;
     
    }
	private void OnDisable()
    {
		output = null;
	}

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (passThrough == false && other.GetComponent<FighterClass>() != null)
        {
            if (other.GetComponent<FighterClass>().teamNumber != fighter.teamNumber)
            {
                yield return new WaitForSeconds(.1f);
                gameObject.SetActive(false);
  
            }
        }
    }
 
}
