using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {
	public GameObject bar1;
	public GameObject bar2;
	public GameObject bar3;
	public GameObject bar4;
	Image fighter1;
	Image fighter2;
	Image fighter3;
	Image fighter4;

	CharacterSpawning charspawn;
	// Use this for initialization
	void Start () {
		charspawn = FindObjectOfType<CharacterSpawning> ();
		fighter1 = bar1.transform.GetComponentInChildren<Image> ();
		fighter2 = bar2.transform.GetComponentInChildren<Image> ();
		fighter3 = bar3.transform.GetComponentInChildren<Image> ();
		fighter4 = bar4.transform.GetComponentInChildren<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (charspawn.players.Count == 0) {
			fighter1.enabled  = false;
			fighter2.enabled = false;
			fighter3.enabled = false;
			fighter4.enabled = false;
		}
		if (charspawn.players.Count >= 1) {
			fighter1.sprite = charspawn.players [0].GetComponent<FighterClass> ().charSprite;
			fighter1.enabled  = true;
		} 
		if (charspawn.players.Count >= 2) {
			fighter2.sprite = charspawn.players [1].GetComponent<FighterClass> ().charSprite;
			fighter2.enabled = true;
		} 
		if (charspawn.players.Count >= 3) {
			fighter3.sprite = charspawn.players [2].GetComponent<FighterClass> ().charSprite;
			fighter3.enabled = true;
		}
		if(charspawn.players.Count >= 4){
			fighter4.sprite = charspawn.players [3].GetComponent<FighterClass> ().charSprite;
			fighter4.enabled = true;
		}
	}
}
