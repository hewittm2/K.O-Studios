//Torrel L. Ethan Q.
//3/27/2019
//Projectile Damage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFighterReference : MonoBehaviour
{
    public FighterClass fighter;
    [Range(-20,20)]
    public float xSpeed;
    [Range(-20, 20)]
    public float ySpeed;

    private void OnEnable()
    {
        if (fighter == null)
        {
            fighter = GetComponentInParent<FighterClass>();
        }
        if (gameObject.tag == "Untagged")
        {
            if (fighter.teamNumber == 1)
                gameObject.tag = "attack1";
            else
                gameObject.tag = "attack2";
        }
        if (fighter.coupDeGraceActivated == true)
        {
            GetComponent<Rigidbody>().AddForce(xSpeed * 100, ySpeed * 50, 0);
        }
     
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<FighterClass>() != null) //Incase it hits something thats NOT a player. I.E Boundary Walls
        {
            if (col.GetComponent<FighterClass>().teamNumber != fighter.teamNumber)
            {
                if (fighter.coupDeGraceActivated == false)
                {
                    //If it hits a player that ISN'T on the same team, it will disable itself.
                    //To prevent it from hitting multiple times
                    gameObject.SetActive(false);
                    //Resets Rigidbody
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }
}
