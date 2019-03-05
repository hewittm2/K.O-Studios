﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour {

	// Use this for initialization
	//the player this hurtbox belongs to
	private FighterClass player;
	public GameObject hitSpark;
	public GameObject blockSpark;
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftKnee;
	public GameObject rightKnee;
	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject leftElbow;
	public GameObject rightElbow;
	public GameObject chest;
	public GameObject head;
	[HideInInspector]
	public List<GameObject> hitBoxes;

	[HideInInspector]
	public string attacker;

	public BaseMovement movement;


	private void Start(){
		player = GetComponent<FighterClass>();
		movement = GetComponent<BaseMovement> ();
		hitSpark = Instantiate (hitSpark,player.gameObject.transform.position, Quaternion.identity);
		blockSpark = Instantiate (blockSpark, player.gameObject.transform.position, Quaternion.identity);
		hitSpark.SetActive (false);
		blockSpark.SetActive (false);

		if (leftFoot != null) 
			hitBoxes.Add (leftFoot);
		if (rightFoot != null) 
			hitBoxes.Add (rightFoot);
		if (leftKnee != null)
			hitBoxes.Add (leftKnee);
		if (rightKnee != null) 
			hitBoxes.Add (rightKnee);
		if (leftHand != null)
			hitBoxes.Add (leftHand);
		if (rightHand != null) 
			hitBoxes.Add (rightHand);
		if (leftElbow != null) 
			hitBoxes.Add (leftElbow);
		if (rightElbow != null)
			hitBoxes.Add (rightElbow);
		if (chest != null)
			hitBoxes.Add (chest);
		if (head != null) 
			hitBoxes.Add (head);

		foreach (GameObject g in hitBoxes) {
			string attackLabel = "attack" + player.teamNumber;
			g.tag = attackLabel;
			g.SetActive(false);
		}
		//player = GetComponentInParent<FighterClass>();
		if (player.teamNumber == 1) {
			attacker = "attack2";
		} else {
			attacker = "attack1";
		}
	}
	private void OnCollisionEnter(Collision col){
		foreach (ContactPoint contact in col.contacts) {
			if (player.canRecieveDamage) {
				if (contact.otherCollider.tag == attacker) {
					if (!player.blocking) {
						ReceiveDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage,col.gameObject.GetComponentInParent<FighterClass> ().knockBack, col.gameObject.GetComponentInParent<FighterClass> ().knockBackForce);
						hitSpark.transform.position = contact.otherCollider.transform.position;
						hitSpark.SetActive(true);

					} else {
						ReceiveBlockedDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage,col.gameObject.GetComponentInParent<FighterClass> ().knockBack, col.gameObject.GetComponentInParent<FighterClass> ().knockBackForce);
						blockSpark.transform.position = contact.otherCollider.transform.position;
						blockSpark.SetActive(true);
					}
				}
			}
		}
	}
	public void ReceiveBlockedDamage(float damage, Vector3 direction, float force){
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		damage *= player.defValue;
		player.currentHealth -= Mathf.RoundToInt(damage);


		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		StartCoroutine (hitDelay (player));

	}
	public void ReceiveDamage(float damage, Vector3 direction, float force){

		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		player.currentHealth -= Mathf.RoundToInt(damage);
		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		StartCoroutine (hitDelay (player));


	}
	IEnumerator hitDelay(FighterClass player){
		movement.dashing = true;
		movement.character.enabled = false;
		movement.rigid.constraints = RigidbodyConstraints.None;
		movement.rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		movement.rigid.velocity = new Vector3(0, 0, 0);
		movement.rigid.angularVelocity = new Vector3(0, 0, 0);
		//movement.rigid.velocity += (new Vector3(dashSpeed * direction,0, 0));
		//yield return new WaitForSeconds(dashSpeed/100);
		movement.rigid.velocity = new Vector3(0, 0, 0);
		movement.rigid.angularVelocity = new Vector3(0, 0, 0);
		yield return new WaitForSeconds(.01f);
		movement.rigid.constraints = RigidbodyConstraints.FreezeAll;
		movement.character.enabled = true;
		movement.dashing = false;
		movement.fighter.canMove = true;
		yield return new WaitForSeconds (1f);
		player.canRecieveDamage = true;
		player.canMove = true;
		player.canAttack = true;
		blockSpark.SetActive (false);
		hitSpark.SetActive (false);
	}

}