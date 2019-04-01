using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public float nRand = 0;
	void Update ()
    {
        nRand = Random.Range(4f, 5f);
        this.transform.GetComponent<Light>().intensity = nRand;
	}
}
