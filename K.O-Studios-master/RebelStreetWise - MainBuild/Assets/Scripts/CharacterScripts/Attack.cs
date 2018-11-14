using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool canHit = true;
    public bool hasHit = false;

    public BoxCollider[] hitboxes = new BoxCollider[0];

    [HideInInspector] public int attackTime = 1;

    private void Update()
    {
        PlayerAttack();
    }

    public void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canHit)
        {
            StartCoroutine(AttackDelay(hitboxes[0], attackTime));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && canHit)
        {
            StartCoroutine(AttackDelay(hitboxes[1], attackTime));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && canHit)
        {
            StartCoroutine(AttackDelay(hitboxes[2], attackTime));
        }
    }

    public void CallDelay()
    {
        StartCoroutine(AttackDelay(hitboxes[0], attackTime));
    }

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
