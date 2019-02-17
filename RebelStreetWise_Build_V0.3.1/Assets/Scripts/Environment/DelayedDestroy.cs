using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {
	public float duration;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
