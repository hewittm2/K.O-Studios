//Jake Poshepny
//11 20 18

/*INSTRUCTIONS
 * Place on the Manager object
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    private CharacterSpawning cs;

    private void Awake()
    {
        cs = GameObject.FindGameObjectWithTag("CrossSceneManager").GetComponent<CharacterSpawning>();
    }

    //Set the Player Character to the Appropriate Character according to the UI Elements
    public void SetPlayerCharacter(int player, GameObject character)
    {
        switch (player)
        {
            case 1:
                cs.playerOne = character;
                break;
            case 2:
                cs.playerTwo = character;
                break;
            case 3:
                cs.playerThree = character;
                break;
            case 4:
                cs.playerFour = character;
                break;
        }
    }
}
