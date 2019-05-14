using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawning : MonoBehaviour
{
    public GameObject player;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
	public List<GameObject> players;
    public List<Sprite> portraits;
    public void CallSpawn(List<Transform> spawns)
    {
        StartCoroutine(Spawn(spawns));
    }

    public IEnumerator Spawn(List<Transform> spawns)
    {
        for (int i = 0; i < spawns.Count; i++)
        {

            if (players[i] != null)
            {
                var emptyObject = new GameObject();
                emptyObject.transform.position = spawns[i].transform.position;
                player = Instantiate(players[i], emptyObject.transform);
                player.transform.SetParent(null);
                if (i == 0) {
                    player.GetComponent<UniversalCharInput>().PlayerOne = true;
                    player.GetComponent<FighterClass>().teamNumber = 1;
                    player.GetComponent<FighterClass>().playerNumber = 1;
                    player.gameObject.tag = "Player1";
                }else if(i == 1) {
                    player.GetComponent<UniversalCharInput>().PlayerTwo = true;
                    player.GetComponent<FighterClass>().teamNumber = 1;
                    player.GetComponent<FighterClass>().playerNumber = 2;
                    player.gameObject.tag = "Player2";
                }
                else if (i == 2)
                {
                    player.GetComponent<UniversalCharInput>().PlayerThree = true;
                    player.GetComponent<FighterClass>().teamNumber = 2;
                    player.GetComponent<FighterClass>().playerNumber = 3;
                    player.gameObject.tag = "Player3";
                }
                else if (i == 3)
                {
                    player.GetComponent<UniversalCharInput>().PlayerFour = true;
                    player.GetComponent<FighterClass>().teamNumber = 2;
                    player.GetComponent<FighterClass>().playerNumber = 4;
                    player.gameObject.tag = "Player4";
                }

                //player1.GetComponent<PlayerInfo>().health = 100;



            }



    //        switch (i){
    //            //Team 1
    //            case 1:
    //                if (players[0] != null){
				//		player1 = Instantiate(players[0], spawns[i - 1]);
				//		player1.transform.SetParent (null);
    //                    player1.GetComponent<UniversalCharInput>().PlayerOne = true;
    //                    //player1.GetComponent<PlayerInfo>().health = 100;
    //                    player1.GetComponent<FighterClass>().teamNumber = 1;
				//		player1.GetComponent<FighterClass> ().playerNumber = 1;
				//		player1.gameObject.tag = "Player1";
            

    //                }
    //                break;
    //            case 2:
				//    if (players[1] != null){
				//	    player2 = Instantiate(players[1], spawns[i - 1]);
				//		player2.transform.SetParent (null);
    //                    player2.GetComponent<UniversalCharInput>().PlayerTwo = true;
    //                    //player2.GetComponent<PlayerInfo>().health = 100;
    //                    player2.GetComponent<FighterClass>().teamNumber = 1;
				//		player2.GetComponent<FighterClass> ().playerNumber = 2;
    //                    player2.gameObject.tag = "Player2";
                   
    //                }
    //                break;
    //            //Team 2
    //            case 3:
    //                if (players[2] != null)
    //                {
    //                    player3 = Instantiate(players[2], spawns[i - 1]);
				//		player3.transform.SetParent (null);
    //                    player3.GetComponent<UniversalCharInput>().PlayerThree = true;
    //                    //player3.GetComponent<PlayerInfo>().health = 100;
    //                    player3.GetComponent<FighterClass>().teamNumber = 2;
				//		player3.GetComponent<FighterClass> ().playerNumber = 3;
				//		player3.GetComponent<FighterClass> ().ToggleDirection();
    //                    player3.gameObject.tag = "Player3";
                 
    //                }
    //                break;
    //            case 4:
				//if (players [3] != null)
    //                {
				//        player4 = Instantiate (players [3], spawns [i - 1]);
				//	    player4.transform.SetParent (null);
				//	    player4.GetComponent<UniversalCharInput> ().PlayerFour = true;
				//	    //player4.GetComponent<PlayerInfo>().health = 100;
				//	    player4.GetComponent<FighterClass> ().teamNumber = 2;
    //                    player3.GetComponent<FighterClass>().playerNumber = 4;
    //                    player4.GetComponent<FighterClass> ().ToggleDirection ();
    //                    player4.gameObject.tag = "Player4";
                  
    //                }
				//break;
                    yield return null;
   //         }
        }
    }
}
