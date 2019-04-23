using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRotation : MonoBehaviour
{
    public int rotationSpeed;
	
	void Update ()
    {
        transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
	}
}
