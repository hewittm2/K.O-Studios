//Created by Ryan Van Dusen 10/31/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSpawning : MonoBehaviour {
    //CharacterPrefabs
    public GameObject characterOne;
    public GameObject characterTwo;
    public GameObject characterThree;
    public GameObject characterFour;
    //SpawnPoints for players
    public Transform player1SpawnLocation;
    public Transform player2SpawnLocation;
    public Transform player3SpawnLocation;
    public Transform player4SpawnLocation;
    //CharacterNames Storage
     string character1;
     string character2;
     string character3;
     string character4;
    //SpawnedCharacters
    public GameObject[] spawnedCharacters  = new GameObject[4];

    private GameManagerAlpha characterSelection;
    private UniversalCharInput controllerAssign;
 
    // Use this for initialization
    void Start ()
    {
        characterSelection = GetComponent<GameManagerAlpha>();
        character1 = characterOne.name;
        character2 = characterTwo.name;
        character3 = characterThree.name;
        character4 = characterFour.name;
        //debugs characterselection
        Debug.Log(characterSelection.characterNames[0]);
        Debug.Log(characterSelection.characterNames[1]);
        Debug.Log(characterSelection.characterNames[2]);
        Debug.Log(characterSelection.characterNames[3]);
        Debug.Log("--------------------------");
        Debug.Log(character1);
        Debug.Log(character2);
        Debug.Log(character3);
        Debug.Log(character4);
        //spawns player

        AllSpawn();


    }

    // Update is called once per frame
    void Update() {

  

    }
    //instantiate characte rand assign to player controller
    void AllSpawn()
    {
        Player1Spawn();
        Player2Spawn();
        Player3Spawn();
        Player4Spawn();
    }
    void Player1Spawn()
    {
      
        

        if (characterSelection.characterNames[0] == character1)
        {
            characterSelection.players[0] = Instantiate(characterOne, player1SpawnLocation);
            characterSelection.players[0].GetComponent<UniversalCharInput>().PlayerOne = true;
        }
        if (characterSelection.characterNames[0] == character2)
        {
            characterSelection.players[0] = Instantiate(characterTwo, player1SpawnLocation);
            characterSelection.players[0].GetComponent<UniversalCharInput>().PlayerOne = true;
        }
        if (characterSelection.characterNames[0] == character3)
        {
            characterSelection.players[0] = Instantiate(characterThree, player1SpawnLocation);
            characterSelection.players[0].GetComponent<UniversalCharInput>().PlayerOne = true;
        }
        if (characterSelection.characterNames[0] == character4)
        {
            characterSelection.players[0] = Instantiate(characterFour, player1SpawnLocation);
            characterSelection.players[0].GetComponent<UniversalCharInput>().PlayerOne = true;
        }
    }
    void Player2Spawn()
    {

        if (characterSelection.characterNames[1] == character1)
        {
            characterSelection.players[1] = Instantiate(characterOne, player2SpawnLocation);
            characterSelection.players[1].GetComponent<UniversalCharInput>().PlayerTwo = true;
        }
        if (characterSelection.characterNames[1] == character2)
        {
            characterSelection.players[1] = Instantiate(characterTwo, player2SpawnLocation);
            characterSelection.players[1].GetComponent<UniversalCharInput>().PlayerTwo = true;
        }
        if (characterSelection.characterNames[1] == character3)
        {
            characterSelection.players[1]  = Instantiate(characterThree, player2SpawnLocation);
            characterSelection.players[1].GetComponent<UniversalCharInput>().PlayerTwo = true;
        }
        if (characterSelection.characterNames[1] == character4)
        {
            spawnedCharacters[1]  = Instantiate(characterFour, player2SpawnLocation);
            characterSelection.players[1].GetComponent<UniversalCharInput>().PlayerTwo = true;
        }
    }
    void Player3Spawn()
    {

        if (characterSelection.characterNames[2] == character1)
        {
            characterSelection.players[2] = Instantiate(characterOne, player3SpawnLocation);
            characterSelection.players[2].GetComponent<UniversalCharInput>().PlayerThree = true;
        }
        if (characterSelection.characterNames[2] == character2)
        {
            characterSelection.players[2] =  Instantiate(characterTwo, player3SpawnLocation);
            characterSelection.players[2].GetComponent<UniversalCharInput>().PlayerThree = true;
        }
        if (characterSelection.characterNames[2] == character3)
        {
            characterSelection.players[2] =  Instantiate(characterThree, player3SpawnLocation);
            characterSelection.players[2].GetComponent<UniversalCharInput>().PlayerThree = true;
        }
        if (characterSelection.characterNames[2] == character4)
        {
            characterSelection.players[2] =  Instantiate(characterFour, player3SpawnLocation);
            characterSelection.players[2].GetComponent<UniversalCharInput>().PlayerThree = true;
        }
    }
    void Player4Spawn()
    {

        if (characterSelection.characterNames[3] == character1)
        {
            characterSelection.players[3] = Instantiate(characterOne, player4SpawnLocation);
            characterSelection.players[3].GetComponent<UniversalCharInput>().PlayerFour = true;
        }
        if (characterSelection.characterNames[3] == character2)
        {
            characterSelection.players[3] = Instantiate(characterTwo, player4SpawnLocation);
            characterSelection.players[3].GetComponent<UniversalCharInput>().PlayerFour = true;
        }
        if (characterSelection.characterNames[3] == character3)
        {
            characterSelection.players[3] = Instantiate(characterThree, player4SpawnLocation);
            characterSelection.players[3].GetComponent<UniversalCharInput>().PlayerFour = true;
        }
        if (characterSelection.characterNames[3] == character4)
        {
            characterSelection.players[3] = Instantiate(characterFour, player4SpawnLocation);
            characterSelection.players[3].GetComponent<UniversalCharInput>().PlayerFour = true;
        }

    }

}
