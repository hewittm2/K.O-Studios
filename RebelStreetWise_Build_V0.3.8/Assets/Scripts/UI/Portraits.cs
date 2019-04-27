using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portraits : MonoBehaviour {

    CharacterSpawning characterSpawning;
    public Image sprite1;
    public Image sprite2;
    public Image sprite3;
    public Image sprite4;

    // Use this for initialization
    void Start () {
        characterSpawning = FindObjectOfType<CharacterSpawning>();
        sprite1.sprite = characterSpawning.portraits[0];
        sprite2.sprite = characterSpawning.portraits[1];
        sprite3.sprite = characterSpawning.portraits[2];
        sprite4.sprite = characterSpawning.portraits[3];
    }
}
