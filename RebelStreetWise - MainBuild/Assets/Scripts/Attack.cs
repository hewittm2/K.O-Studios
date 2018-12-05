using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool canHit = true;
    public bool hasHit = false;

    public BoxCollider[] hurtboxes = new BoxCollider[0];

    [HideInInspector] public int attackTime = 1;

    public void CallDelay()
    {
        StartCoroutine(AttackDelay(hurtboxes[0], attackTime));
    }

    //DONT CHANGE THIS IT WORKS 
    public IEnumerator AttackDelay(BoxCollider hitbox, int cooldown)
    {
        canHit = false;
        hasHit = true;
        hitbox.enabled = true;
        yield return new WaitForSeconds(.1f);
        hitbox.enabled = false;
        yield return new WaitForSeconds(attackTime * .9f);
        canHit = true;
        hasHit = false;
    }
}
