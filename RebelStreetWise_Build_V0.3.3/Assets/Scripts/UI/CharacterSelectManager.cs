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
                cs.players[0] = character;
                break;
            case 2:
                cs.players[1] = character;
                break;
            case 3:
                cs.players[2] = character;
                break;
            case 4:
                cs.players[3] = character;
                break;
        }
    }
}
