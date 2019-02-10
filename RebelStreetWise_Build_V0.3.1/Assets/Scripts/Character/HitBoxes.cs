using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour {

	// Use this for initialization
	//the player this hurtbox belongs to
	private FighterClass playerInfo;
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
	public List<GameObject> hitBoxes;

	private void Start(){
		playerInfo = GetComponent<FighterClass>();
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
			string attackLabel = "attack" + playerInfo.teamNumber;
			g.tag = attackLabel;
			g.SetActive(false);
		}
	}
	void Attack(Collider limb, int damage){
	
	}
	// Update is called once per frame
	void Update () {

	}
}
