using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
	private FighterClass player;
	[HideInInspector]
	public string attacker;
	public GameObject hitSpark;
	public GameObject blockSpark;
	// Use this for initialization
	void Start () {
		player = GetComponentInParent<FighterClass>();
		if (player.teamNumber == 1) {
			attacker = "attack2";
		} else {
			attacker = "attack1";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnCollisionEnter(Collision col){
		foreach (ContactPoint contact in col.contacts) {
			if (player.canRecieveDamage) {
				if (contact.otherCollider.tag == attacker) {
					if (!player.blocking) {
						ReceiveDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage);
						Instantiate (hitSpark, col.gameObject.transform.position, Quaternion.identity);
					} else {
						ReceiveDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage);
						Instantiate (blockSpark, col.gameObject.transform.position, Quaternion.identity);
					}

				}
			}
		}
	}
	public void RecieveBlockedDamage(float damage){
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		player.currentHealth -= Mathf.RoundToInt(damage/player.defValue);


		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		player.canRecieveDamage = true;
		player.canMove = true;
		player.canAttack = true;
	}
	public void ReceiveDamage(float damage){
		
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		player.currentHealth -= Mathf.RoundToInt(damage);
		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		player.canRecieveDamage = true;
		player.canMove = true;
		player.canAttack = true;
	}
}
	