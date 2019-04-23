//Updated Torrel 4/22/2018
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

	void Start () {
		charspawn = FindObjectOfType<CharacterSpawning> ();
		fighter1 = bar1.transform.GetComponentInChildren<Image> ();
		fighter2 = bar2.transform.GetComponentInChildren<Image> ();
		fighter3 = bar3.transform.GetComponentInChildren<Image> ();
		fighter4 = bar4.transform.GetComponentInChildren<Image> ();

        //Scene persist checker. Just removes all characters when going back to the scene.
        CharacterSpawning characterSelections = FindObjectOfType<CharacterSpawning>();
        if (characterSelections.players.Count > 0)
        {
            characterSelections.players.Clear();
            characterSelections.portraits.Clear();
        }
    }
    public void DeSelectCharacter()
    {
        CharacterSpawning characterSelections = FindObjectOfType<CharacterSpawning>();
        if (characterSelections.players.Count > 0)
        {
            characterSelections.players.RemoveAt(characterSelections.players.Count - 1);
            characterSelections.portraits.RemoveAt(characterSelections.portraits.Count - 1);
            if (characterSelections.players.Count + 1 == 1)
                fighter1.enabled = false;
            if (characterSelections.players.Count + 1 == 2)
                fighter2.enabled = false;
            if (characterSelections.players.Count + 1 == 3)
                fighter3.enabled = false;
            if (characterSelections.players.Count + 1 == 4)
                fighter4.enabled = false;
        }
    }
    void Update ()
    {
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

        if (Input.GetButtonDown("Back_1") || Input.GetButtonDown("Back_2") || Input.GetButtonDown("Back_3") || Input.GetButtonDown("Back_4"))
        {
            DeSelectCharacter();
        }
	}
}
