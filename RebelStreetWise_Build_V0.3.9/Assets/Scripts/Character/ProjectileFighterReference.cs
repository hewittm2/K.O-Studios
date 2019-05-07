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
	[Range(-20,20)]
	public float xSpeed;
	[Range(-20,20)]
	public float ySpeed;
	public bool hit;

    private void OnEnable(){
		hit = false;
        if (fighter == null)
            fighter = GetComponentInParent<FighterClass>();

		if (gameObject.tag == "Untagged") {
			if (fighter.teamNumber == 1) 
				gameObject.tag = "attack1";
			else
				gameObject.tag = "attack2";	
		}
		if (fighter.coupDeGraceActivated)
			GetComponent<Rigidbody> ().AddForce (xSpeed * 100, ySpeed * 50, 0);


		output = fighter.output;
     
    }
	private void OnDisable(){
		output = null;
	}
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
}
