using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
	private FighterClass player;
	[HideInInspector]
	public string attacker;
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
					ReceiveDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage);
				}
			}
		}
	}

	public void ReceiveDamage(int damage){
		player.canRecieveDamage = false;
		player.canMove = false;
		player.canAttack = false;
		player.currentHealth -= damage;
		Mathf.RoundToInt(player.currentHealth);

		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
		player.canRecieveDamage = true;
		player.canMove = true;
		player.canAttack = true;
	}
}
	