using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawning : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public void CallSpawn(List<Transform> spawns)
    {
        StartCoroutine(Spawn(spawns));
    }

    public IEnumerator Spawn(List<Transform> spawns)
    {
        yield return new WaitForSeconds(.01f);

        for (int i = 1; i < spawns.Count + 1; i++)
        {
            switch (i)
            {
                //Team 1
                case 1:
                    if (playerOne != null)
                    {
                        GameObject player1;
                        player1 = Instantiate(playerOne, spawns[i - 1]);
                        player1.GetComponent<UniversalCharInput>().PlayerOne = true;
                        player1.GetComponent<PlayerInfo>().health = 100;
                        player1.GetComponent<PlayerInfo>().team = 1;
                        player1.gameObject.tag = "Player1";
                    }
                    break;
                case 2:
                    if (playerTwo != null)
                    {
                        GameObject player2;
                        player2 = Instantiate(playerTwo, spawns[i - 1]);
                        player2.GetComponent<UniversalCharInput>().PlayerTwo = true;
                        player2.GetComponent<PlayerInfo>().health = 100;
                        player2.GetComponent<PlayerInfo>().team = 1;
                        player2.gameObject.tag = "Player2";
                    }
                    break;
                //Team 2
                case 3:
                    if (playerThree != null)
                    {
                        GameObject player3;
                        player3 = Instantiate(playerThree, spawns[i - 1]);
                        player3.GetComponent<UniversalCharInput>().PlayerThree = true;
                        player3.GetComponent<PlayerInfo>().health = 100;
                        player3.GetComponent<PlayerInfo>().team = 2;
                        player3.gameObject.tag = "Player3";
                    }
                    break;
                case 4:
                    if (playerOne != null)
                    {
                        GameObject player4;
                        player4 = Instantiate(playerFour, spawns[i - 1]);
                        player4.GetComponent<UniversalCharInput>().PlayerFour = true;
                        player4.GetComponent<PlayerInfo>().health = 100;
                        player4.GetComponent<PlayerInfo>().team = 2;
                        player4.gameObject.tag = "Player4";
                    }
                    break;
            }
        }
    }
}
