//Jake Poshepny
//11 20 18

/*INSTRUCTIONS
 * Place on the GameManager object
 * When you want a scene transition, set up the button using the GameManager > Manager > ChangeScene function
 * Set the string value to be the name of the scene you want to switch to such as: Main Menu or MainMenu or Level1
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
