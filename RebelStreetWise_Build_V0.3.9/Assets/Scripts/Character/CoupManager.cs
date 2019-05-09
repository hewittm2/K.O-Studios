using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupManager : MonoBehaviour
{
    [Range(0,1200f)]
    public float t1SuperMeter;
    [Range(0, 1200f)]
    public float t2SuperMeter;

    public int t1 = 0;
    public int t2 = 0;

    public bool t1CoupSuccessful;
    public bool t2CoupSuccessful;
    void Start ()
    {
		
	}
	

	void Update ()
    {
        if (t1 == 2)
            t1CoupSuccessful = true;
        else
            t1CoupSuccessful = false;

        if (t2 == 2)
            t2CoupSuccessful = true;
        else
            t2CoupSuccessful = false;

    }
}
