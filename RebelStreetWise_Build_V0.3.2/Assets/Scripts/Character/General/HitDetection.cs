using System.Collections;
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
	[HideInInspector]
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
					FighterClass attacker = col.gameObject.GetComponentInParent<FighterClass>();
					if (!player.blocking) {
						ReceiveDamage (attacker.damage,attacker.knockBackDirection, attacker.knockBackForce);
						hitSpark.transform.position = contact.otherCollider.transform.position;
						hitSpark.SetActive(true);
						attacker.superMeter += attacker.meterGain;
					} else {
						ReceiveBlockedDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage,col.gameObject.GetComponentInParent<FighterClass> ().knockBackDirection, col.gameObject.GetComponentInParent<FighterClass> ().knockBackForce);
						blockSpark.transform.position = contact.otherCollider.transform.position;
						blockSpark.SetActive(true);
						attacker.superMeter += (attacker.meterGain*player.defValue);
					}
				}
			}
		}
	}
	public void ReceiveBlockedDamage(float damage, Vector3 kbDirection, float kbForce){
		player.anim.SetTrigger("GetHit");
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		damage *= player.defValue;
		player.currentHealth -= Mathf.RoundToInt(damage);


		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		StartCoroutine (hitDelay (player, kbDirection,kbForce));

	}
	public void ReceiveDamage(float damage, Vector3 kbDirection, float kbForce){
		player.anim.SetTrigger("GetHit");
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		player.currentHealth -= Mathf.RoundToInt(damage);
		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		StartCoroutine (hitDelay (player,kbDirection, kbForce));


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