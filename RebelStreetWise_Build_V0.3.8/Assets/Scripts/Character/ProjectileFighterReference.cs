//Torrel L. Ethan Q.
//4/8/2019
//Projectile Damage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFighterReference : MonoBehaviour
{
    public FighterClass fighter;
	public FighterClass.AttackStats output;

    private void OnEnable()
    {
        if (fighter == null)
        {
            fighter = GetComponentInParent<FighterClass>();
        }
		output = fighter.output;
     
    }
	private void OnDisable(){
		output = null;
	}
}
