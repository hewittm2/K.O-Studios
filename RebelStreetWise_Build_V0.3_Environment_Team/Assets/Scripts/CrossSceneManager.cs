using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneManager : MonoBehaviour 
{
	private static CrossSceneManager csm = null;
	public static CrossSceneManager Csm { get { return csm; } }

	private void Awake()
	{
		//Checks to see if a CSM already exists
        if (csm != null && csm != this) {
            //If so, destroy this one
            Destroy(this.gameObject);
            return;
        }
        else {
            //Else, make this the Main CSM
            csm = this; 
        }

        //Make this object persist between scenes
        DontDestroyOnLoad(this);
	}
}
