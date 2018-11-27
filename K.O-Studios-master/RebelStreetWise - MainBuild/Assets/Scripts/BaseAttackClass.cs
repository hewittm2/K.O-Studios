using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackClass : MonoBehaviour {
	public float damage;
	public float blockedDamage;
	public Vector2 knockBack;
	public Vector2 blockedKnockBack;
	public Animation attackAnim;
	public float hitStunDuration;
	public float blockedHitStunDuration;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(BoxCollider hit){
		
	}
}
