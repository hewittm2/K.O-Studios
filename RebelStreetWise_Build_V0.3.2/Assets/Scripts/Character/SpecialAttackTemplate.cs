using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTemplate : MonoBehaviour
{
    [System.Serializable]
    public class SpecialAttacks
    {
		[Range(.1f, 2f)]
		public float animSpeed;
        public enum HitType { High, Mid, Low }
        public HitType hitType;
        public enum DamageType { Hit, Stun, KnockDown }
        public DamageType damageType;
        public Vector3 knockback;
        public float knockbackForce;
        public int damage;
        public float startupTime;
        public float activeTime;
        public int stunTime;
        public float recoveryTime;
        public GameObject partEffect;
    }

    [System.Serializable]
    public class SpecialAttackStats
    {
        public SpecialAttacks SpecialForward = new SpecialAttacks();
        public SpecialAttacks SpecialBack = new SpecialAttacks();
        public SpecialAttacks SpecialDown = new SpecialAttacks();
        public SpecialAttacks SpecialNeutral = new SpecialAttacks();
        public SpecialAttacks SpecialJump = new SpecialAttacks();
        public SpecialAttacks SpecialBreakdown = new SpecialAttacks();
    }
    public SpecialAttackStats specialAttackStats = new SpecialAttackStats();

    public virtual void NeutralSA(SpecialAttacks neutral) //Neutral Special Attack
    {

    }
    public virtual void ForwardSA(SpecialAttacks forward) //Forward Special Attack
    {

    }
    public virtual void BackSA(SpecialAttacks back) //Back Special Attack
    {

    }
    public virtual void DownSA(SpecialAttacks down) //Down Special Attack
    {

    }
    public virtual void JumpSA(SpecialAttacks jump) //Jump Special Attack
    {

    }
    public virtual void BreakdownSA(SpecialAttacks breakdown) //Breakdown
    {

    }
}
