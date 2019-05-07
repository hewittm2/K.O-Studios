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
<<<<<<< HEAD
<<<<<<< HEAD
	private void OnTriggerEnter(Collider col){
		if (col.GetComponent<FighterClass> () != null) {
			if (col.GetComponent<FighterClass> ().teamNumber != fighter.teamNumber) {
				if (fighter.coupDeGraceActivated == false) {
					gameObject.SetActive (false);
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					hit = true;
				}
			}
		}

	}
=======
>>>>>>> parent of a3e2d5d... 2 script updates
=======
>>>>>>> parent of 8be7939... Merge branch 'Master_Implementation' of https://github.com/hewittm2/K.O-Studios into Master_Implementation
}
