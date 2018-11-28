using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour {
    public GameObject popup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //For bringing up the overlay
        if (Input.GetKeyDown(KeyCode.Q))
        {
            popup.SetActive(true);
        }
		
	}

}
