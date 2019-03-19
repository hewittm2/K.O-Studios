using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTemplate : MonoBehaviour
{
    private FighterClass self;

    //Apollo
    [System.Serializable]
    public class SpecialAttacks
    {
        public Vector3 knockback;
        public float knockbackForce;
        public float startUpTime;
        public float activeTime;        
        public float recoveryTime;
        public int stunTime;
        public float damage;
        public ParticleSystem partEffect;
    }
    public SpecialAttacks specialAttackVars = new SpecialAttacks();
    
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

    private void Start()
    {
        self = GetComponent<FighterClass>();
    }

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
