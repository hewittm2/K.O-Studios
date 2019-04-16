using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireknifeSpin : MonoBehaviour {
	
	public GameObject FireKnife1;
    public GameObject FireKnife2;
	public float rotationSpeed;
	private Vector3 rotation;

	// Use this for initialization
	void Update () {
		FireKnife1.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        FireKnife2.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

    }

}
