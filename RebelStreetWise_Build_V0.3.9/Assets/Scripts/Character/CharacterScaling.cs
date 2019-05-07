using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScaling : MonoBehaviour {
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
    public GameObject Character4;
    public Vector3 Size = new Vector3(.2f,.2f,.2f);
	// Use this for initialization
	void Start () {
        Character1 = GameObject.FindWithTag("Player1");
        Character2 = GameObject.FindWithTag("Player2");
        Character3 = GameObject.FindWithTag("Player3");
        Character4 = GameObject.FindWithTag("Player4");

        Character1.transform.localScale = Size;
        Character2.transform.localScale = Size;
        Character3.transform.localScale = Size;
        Character4.transform.localScale = Size;

        //Character1.transform.eulerAngles = new Vector3(0, 0, 0);
        //Character2.transform.eulerAngles = new Vector3(0, 0, 0);
        //Character3.transform.eulerAngles = new Vector3(0, 0, 0);
        //Character4.transform.eulerAngles = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
