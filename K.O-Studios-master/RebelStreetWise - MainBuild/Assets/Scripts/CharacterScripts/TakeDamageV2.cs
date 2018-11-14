using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this script on a HURT BOX

[RequireComponent(typeof(BoxCollider))]
public class TakeDamageV2 : MonoBehaviour
{
    #region Variables
    //inspector dropdown for the location of the hurtbox
    public enum Height
    {
        High,
        Mid,
        Low
    }

    //height variable for setting switch location and damage values
    public Height boxHeight = Height.High;

    //switch location and damage values
    public string height = "High";
    public float damageMult = 2;

    //the player this hurtbox belongs to
    public PlayerHealth playerHealth;
    #endregion

    #region Functions
    private void Start()
    {
        //set location and damage values according to hurtbox height
        switch (boxHeight)
        {
            case Height.High:
                height = "High";
                damageMult = 2f;
                break;

            case Height.Mid:
                height = "Mid";
                damageMult = 1.5f;
                break;

            case Height.Low:
                height = "Low";
                damageMult = 1f;
                break;
        }
    }

    //when this hurtbox is hit
    private void OnTriggerEnter(Collider other)
    {
        GameObject o = other.gameObject;
        HitBoxDamage av2 = o.GetComponent<HitBoxDamage>();

        //TAG CHECK WILL NEED CHANGING, TESTING PURPOSES ONLY
        //if (o.tag == "attack")
        //{
        //    ReceiveDamage(other.gameObject.GetComponent<AttackV2>().Damage);
        //}

        if (o.tag == "attackBall" && av2.attack.canHit)
        {
            if (!av2.attack.hasHit)
            {
                av2.attack.CallDelay();
                ReceiveDamage(av2.Damage);
            }
            else
                return;
        }
        else
            return;
    }

    //apply damage to the player health
    private void ReceiveDamage(int baseDamage)
    {
        //store damage in a variable for easy debug log, can be changed later
        float damageToDeal = (baseDamage * damageMult);

        //reduce the player health and round it to a whole number
        playerHealth.Health -= damageToDeal;
        Mathf.RoundToInt(playerHealth.Health);

        Debug.Log(height + "Box Hit for " + damageToDeal + " points of damage");
    }
    #endregion
}
