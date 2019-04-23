//Created By Ethan Quandt
//Edited 4/8/19
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour {

	// Use this for initialization
	//the player this hurtbox belongs to
	private FighterClass player;
	[Header("Get Hit Particle Effects")][Space(0.5f)]
	public GameObject hitSpark;
	public GameObject blockSpark;
	[Header("Basic Attack Hitboxes")][Space(0.5f)]
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
	[Header("Special Attack HitBoxes")][Space(0.5f)]
	public GameObject specialNeutral;
	public GameObject specialForward;
	public GameObject specialBackward;
	public GameObject specialDown;
	public GameObject specialJump;
	public GameObject[] coupDeGrace;
	[Header("Meter Gain When Damaged")][Space(.5f)]
	public int lightHit;
	public int lightBlock;
	public int mediumHit;
	public int mediumBlock;
	public int heavyHit;
	public int heavyBlock;
	public int specMeleeHit;
	public int specMeleeBlock;
	public int specProjectileHit;
	public int specProjectileBlock;
	public int breakdownHit;
	public int breakdownBlock;


	[HideInInspector]
	public List<GameObject> hitBoxes;

	[HideInInspector]
	public string attackerLabel;
	[HideInInspector]
	public BaseMovement movement;
	[HideInInspector]
	public FighterClass attacker;

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
		if (specialNeutral != null)
			hitBoxes.Add (specialNeutral);
		if (specialForward != null)
			hitBoxes.Add (specialForward);
		if (specialBackward != null)
			hitBoxes.Add (specialBackward);
		if (specialDown != null)
			hitBoxes.Add (specialDown);
		if (specialJump != null)
			hitBoxes.Add (specialJump);
		if (coupDeGrace != null) {
			foreach (GameObject g in coupDeGrace) {
				hitBoxes.Add (g);
			}
		}


		foreach (GameObject g in hitBoxes) {
			string attackLabel = "attack" + player.teamNumber;
			g.tag = attackLabel;
			g.SetActive(false);
		}
		//player = GetComponentInParent<FighterClass>();
		if (player.teamNumber == 1) {
			attackerLabel = "attack2";
		} else {
			attackerLabel = "attack1";
		}
	}
	private void OnCollisionEnter(Collision col){
		foreach (ContactPoint contact in col.contacts) {
			if (player.canRecieveDamage) {
				if (contact.otherCollider.tag == attackerLabel) {
					attacker = col.gameObject.GetComponentInParent<FighterClass> ();
					if (attacker != null) {
						if (!player.blocking) {
							ReceiveDamage (attacker.output);
							hitSpark.transform.position = contact.otherCollider.transform.position;
							hitSpark.SetActive(true);
							attacker.superMeter += attacker.output.meterGain;
						} else {
							ReceiveBlockedDamage (attacker.output);
							blockSpark.transform.position = contact.otherCollider.transform.position;
							blockSpark.SetActive(true);
							attacker.superMeter += (attacker.output.meterGain*player.defValue);
						}
					}

				}
			}
		}
	}
	public void OnTriggerEnter(Collider col){
		Debug.Log ("Projectile Hit");
		if (col.gameObject.tag == attackerLabel) {
			if (col.gameObject.GetComponent<ProjectileFighterReference>()) {
				attacker = col.gameObject.GetComponent<ProjectileFighterReference> ().fighter;
			}
			if (attacker != null) {
				if (!player.blocking) {
					ReceiveDamage (attacker.output);
					hitSpark.transform.position = col.transform.position;
					hitSpark.SetActive(true);
					attacker.superMeter += attacker.output.meterGain;
				} else {
					ReceiveBlockedDamage (attacker.output);
					blockSpark.transform.position = col.transform.position;
					blockSpark.SetActive(true);
					attacker.superMeter += (attacker.output.meterGain*player.defValue);
				}
			}
		}
	}
	public void ReceiveBlockedDamage(FighterClass.AttackStats recievedAttack){
		player.anim.SetTrigger("Light Damage");
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		//recievedAttack.attDam *=  Mathf.RoundToInt(player.defValue);
		player.currentHealth -= Mathf.RoundToInt(recievedAttack.attDam * player.defValue);
		switch (recievedAttack.hitType) {
		case FighterClass.HitType.Light:
			player.superMeter += lightBlock;
				break;
		case FighterClass.HitType.Medium:
			player.superMeter += mediumBlock;
				break;
		case FighterClass.HitType.Heavy:
			player.superMeter += heavyBlock;
				break;
		case FighterClass.HitType.SpecialMelee:
			player.superMeter += specMeleeBlock;
				break;
		case FighterClass.HitType.SpecialProjectile:
			player.superMeter += specProjectileBlock;
				break;
		case FighterClass.HitType.Breakdown:
			player.superMeter += breakdownBlock;
			break;
		}


		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + recievedAttack.attDam + " points of damage");
		StartCoroutine (hitDelay (player, recievedAttack.knockBackDirection,recievedAttack.knockBackForce));

	}
	public void ReceiveDamage(FighterClass.AttackStats recievedAttack){
		//player.anim.SetTrigger("GetHit");
		if (recievedAttack.damageType == FighterClass.DamageType.Hit) {
			player.anim.SetTrigger("Light Damage");
		}else if(recievedAttack.damageType == FighterClass.DamageType.Stun){ 
			player.anim.SetTrigger("Heavy Damage");
		}else if(recievedAttack.damageType == FighterClass.DamageType.KnockDown){ 
			player.anim.SetTrigger("Knock Out");
		}
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;

		player.currentHealth -= Mathf.RoundToInt(recievedAttack.attDam);
		switch (recievedAttack.hitType) {
		case FighterClass.HitType.Light:
			player.superMeter += lightHit;
			break;
		case FighterClass.HitType.Medium:
			player.superMeter += mediumHit;
			break;
		case FighterClass.HitType.Heavy:
			player.superMeter += heavyHit;
			break;
		case FighterClass.HitType.SpecialMelee:
			player.superMeter += specMeleeHit;
			break;
		case FighterClass.HitType.SpecialProjectile:
			player.superMeter += specProjectileHit;
			break;
		case FighterClass.HitType.Breakdown:
			player.superMeter += breakdownHit;
			break;
		}
		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + recievedAttack.attDam + " points of damage",gameObject);
		StartCoroutine (hitDelay (player,recievedAttack.knockBackDirection, recievedAttack.knockBackForce));


	}
	IEnumerator hitDelay(FighterClass player, Vector3 kbDirection, float kbForce){
		movement.dashing = true;
		movement.character.enabled = false;
		movement.rigid.constraints = RigidbodyConstraints.None;
		movement.rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		movement.rigid.velocity = new Vector3(0, 0, 0);
		movement.rigid.angularVelocity = new Vector3(0, 0, 0);
        movement.rigid.velocity += (new Vector3(kbDirection.x,kbDirection.y, 0)*kbForce);
        yield return new WaitForSeconds(.2f);
		movement.rigid.velocity = new Vector3(0, 0, 0);
		movement.rigid.angularVelocity = new Vector3(0, 0, 0);
		yield return new WaitForSeconds(.01f);
		movement.rigid.constraints = RigidbodyConstraints.FreezeAll;

		yield return new WaitForSeconds (.2f);
		attacker = null;
		movement.fighter.canMove = true;
		player.canRecieveDamage = true;
		player.canMove = true;
		player.canAttack = true;
		movement.character.enabled = true;
		movement.dashing = false;
		blockSpark.SetActive (false);
		hitSpark.SetActive (false);
	}

}