using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris B.
// 3/11/2019

public class FireScript : MonoBehaviour
{
    [HideInInspector]
    public bool fireHit;
    [HideInInspector]
    public bool doFireDmg;

    public void OnParticleCollision(GameObject other)
    {
        if(fireHit)
        {
            return;
        }

        if(other.gameObject.layer == 10)
        {
            fireHit = true;
            doFireDmg = true;
        }
    }
}
