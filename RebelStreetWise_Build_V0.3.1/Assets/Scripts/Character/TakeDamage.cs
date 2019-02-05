using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this script on a HURT BOX

[RequireComponent(typeof(BoxCollider))]
public class TakeDamage : MonoBehaviour
{
    #region Variables

    //the player this hurtbox belongs to
    public FighterClass playerInfo;
    [HideInInspector] public GameObject player;
    #endregion

    #region Functions
    private void Start()
    {
        player = playerInfo.transform.gameObject;
    }

    //when this hurtbox is hit
    private void OnTriggerEnter(Collider hitbox)
    {
        GameObject o = hitbox.gameObject;
        AttackDamage atkdmg = o.GetComponent<AttackDamage>();

        if (o.tag == "attackBall" && atkdmg.attack.canHit && atkdmg.playerInfo.team != player.GetComponent<FighterClass>().teamNumber)
        {
            if (!atkdmg.attack.hasHit)
            {
                atkdmg.attack.CallDelay();
                ReceiveDamage(atkdmg.Damage);
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
        int damageToDeal = baseDamage;

        //reduce the player health and round it to a whole number
        playerInfo.currentHealth -= damageToDeal;
        Mathf.RoundToInt(playerInfo.currentHealth);

        Debug.Log("HitBox of Team " + playerInfo.teamNumber + " Hit for " + damageToDeal + " points of damage");
    }
    #endregion
}
