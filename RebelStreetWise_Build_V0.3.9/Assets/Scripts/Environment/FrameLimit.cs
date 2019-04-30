// Created By Mike K
// Edited by : 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimit : MonoBehaviour {

	//public vars and their defaults from my testing
	public int TargetFrameLimit = 30;
	public int VSync = 3;

	// Use this for initialization
	void Awake () {
		//sets target rate and number of vsyncs used by unity to determine frame rate --- Not perfect just sets the ideal FPS for unity to "Try" to achieve
		QualitySettings.vSyncCount = VSync;
		Application.targetFrameRate = TargetFrameLimit;
	}
	
	// Update is called once per frame
	void Update () {
		// assures that the target rate does not change even by unity -- May not be needed
		if(Application.targetFrameRate != TargetFrameLimit){
			Application.targetFrameRate = TargetFrameLimit;
		}
	}
}
