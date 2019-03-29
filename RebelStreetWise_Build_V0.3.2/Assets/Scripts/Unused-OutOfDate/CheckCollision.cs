//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class CheckCollision : MonoBehaviour {
//	private FighterClass player;
//	[HideInInspector]
//	public string attacker;
//	public GameObject hitSpark;
//	public GameObject blockSpark;
//	// Use this for initialization
//	void Start () {
//		player = GetComponentInParent<FighterClass>();
//		if (player.teamNumber == 1) {
//			attacker = "attack2";
//		} else {
//			attacker = "attack1";
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//	private void OnCollisionEnter(Collision col){
//		foreach (ContactPoint contact in col.contacts) {
//			if (player.canRecieveDamage) {
//				if (contact.otherCollider.tag == attacker) {
//					if (!player.blocking) {
//						ReceiveDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage);
//						Instantiate (hitSpark,contact.otherCollider.transform.position, Quaternion.identity);
//					} else {
//						ReceiveBlockedDamage (col.gameObject.GetComponentInParent<FighterClass> ().damage);
//						Instantiate (blockSpark, contact.otherCollider.transform.position, Quaternion.identity);
//					}
//
//				}
//			}
//		}
//	}
//	public void ReceiveBlockedDamage(float damage){
//		player.canRecieveDamage = false;
//		player.canMove = false;
//		player.canAttack = false;
//		damage *= player.defValue;
//		player.currentHealth -= Mathf.RoundToInt(damage);
//
//
//		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
//		StartCoroutine (hitDelay (player));
//	}
//	public void ReceiveDamage(float damage){
//		
//		player.canRecieveDamage = false;
//		player.canMove = false;
//		player.canAttack = false;
//		player.currentHealth -= Mathf.RoundToInt(damage);
//		Debug.Log("HitBox of Team " + player.teamNumber + " Hit for " + damage + " points of damage");
//		StartCoroutine (hitDelay (player));
//
//	}
//	IEnumerator hitDelay(FighterClass player){
//		yield return new WaitForSeconds (1f);
//		player.canRecieveDamage = true;
//		player.canMove = true;
//		player.canAttack = true;
//	}
//}
//	