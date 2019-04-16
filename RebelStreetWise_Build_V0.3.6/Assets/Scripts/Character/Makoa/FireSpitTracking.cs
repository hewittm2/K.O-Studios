using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris B.
// 3/17/2019

public class FireSpitTracking : MonoBehaviour
{
    [HideInInspector]
    public bool fireSpitHit;
    [HideInInspector]
    public bool doFireSpitDmg;
    [HideInInspector]
    public bool knockback;
    [HideInInspector]
    public bool knockdown;

    public void OnParticleCollision(GameObject other)
    {
        if(fireSpitHit)
        {
            return;
        }

        if(other.gameObject.layer == 10)
        {
            GameObject enemy = other.gameObject;
            FighterClass enemyVars = enemy.GetComponent<FighterClass>();

            // If enemy is on the ground, set damage to true

            // ===== NO VARIABLE FOR DETECTING KNOCKED DOWN STATUS ===== \\

            //if(enemyVars.knockedDown)
            //{
            //    doFireSpitDmg = true;
            //}

            // If enemy is in the air, set knockdown to true

            if (enemyVars.GetComponent<BaseMovement>().character.isGrounded == false)
            {
                knockdown = true;
            }

            // If enemy is blocking, set knockback to true
            else if(enemyVars.blocking)
            {
                knockback = true;
            }

            // If player is not blocking, Set damage taking and knockdown to true \\
            else if(enemyVars.blocking == false)
            {
                doFireSpitDmg = true;
                knockdown = true;
            }

            fireSpitHit = true;
        }
    }
}
