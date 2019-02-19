//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class HurtBoxes : MonoBehaviour {
//
//	//the player this hurtbox belongs to
//	private FighterClass playerInfo;
//	public GameObject rightLegLowerHurt;
//	public GameObject rightLegUpperHurt;
//	public GameObject leftLegLowerHurt;
//	public GameObject leftLegUpperHurt;
//	public GameObject rightArmLowerHurt;
//	public GameObject rightArmUpperHurt;
//	public GameObject leftArmLowerHurt;
//	public GameObject leftArmUpperHurt;
//	public GameObject chestHurt;
//	public GameObject headHurt;
//	private List<GameObject> hurtBoxes;
//
//	private void Start(){
//
//		playerInfo = GetComponent<FighterClass>();
//		hurtBoxes.Add (rightLegLowerHurt);
//		hurtBoxes.Add (rightLegUpperHurt);
//		hurtBoxes.Add (leftLegLowerHurt);
//		hurtBoxes.Add (leftLegUpperHurt);
//		hurtBoxes.Add (rightArmLowerHurt);
//		hurtBoxes.Add (rightArmUpperHurt);
//		hurtBoxes.Add (leftArmLowerHurt);
//		hurtBoxes.Add (leftArmUpperHurt);
//		hurtBoxes.Add (chestHurt);
//		hurtBoxes.Add (headHurt);
//		foreach (GameObject g in hurtBoxes) {
//			g.AddComponent<TakeDamage>();
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}
