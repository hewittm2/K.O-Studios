using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    public GameObject character;
    public Sprite portrait;
	public void SelectCharacter(){
		CharacterSpawning characterSelections = FindObjectOfType<CharacterSpawning>();
		if (characterSelections.players.Count < 4) {
			characterSelections.players.Add (character);
            characterSelections.portraits.Add(portrait);
		}
	}
}
