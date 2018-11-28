//Jake Poshepny
//11 20 18

/*INSTRUCTIONS
 * Place on the StageManager object
 * Set the Spawn Locations where you want and link them into
 *      the Manager in the order that you want them to spawn
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private CharacterSpawning cs;

    public List<Transform> spawns = new List<Transform>();

    private void Awake()
    {
        cs = GameObject.FindGameObjectWithTag("CrossSceneManager").GetComponent<CharacterSpawning>();
        cs.CallSpawn(spawns);
    }
}
