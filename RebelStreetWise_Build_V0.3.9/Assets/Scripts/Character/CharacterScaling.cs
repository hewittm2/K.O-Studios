using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScaling : MonoBehaviour {
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
    public GameObject Character4;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
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

        Character1.transform.position = spawn1.transform.position;
        Character2.transform.position = spawn2.transform.position;
        Character3.transform.position = spawn3.transform.position;
        Character4.transform.position = spawn4.transform.position;

      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
