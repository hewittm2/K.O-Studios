using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTemplate : MonoBehaviour
{
    private FighterClass self;

    //Apollo
    [System.Serializable]
    public class SpecialAttackStats
    {
		[Range(.1f, 2f)]
		public float animSpeed;
        public Vector3 knockback;
        public int damage;
        public int stunTime;
        public float recoveryTime;
        public GameObject partEffect;
    }
    
      public SpecialAttackStats SpecialForward = new SpecialAttackStats();
      public SpecialAttackStats SpecialBack = new SpecialAttackStats();
      public SpecialAttackStats SpecialDown = new SpecialAttackStats();
      public SpecialAttackStats SpecialNeutral = new SpecialAttackStats();
      public SpecialAttackStats SpecialJump = new SpecialAttackStats();
      public SpecialAttackStats SpecialBreakdown = new SpecialAttackStats();
    
  

    private void Start()
    {
        self = GetComponent<FighterClass>();
    }

    public virtual void NeutralSA(SpecialAttackStats neutral) //Neutral Special Attack
    {
		Debug.Log ("Neutral_Special");
    }
    public virtual void ForwardSA(SpecialAttackStats forward) //Forward Special Attack
    {
		Debug.Log ("Forward_Special");
    }
    public virtual void BackSA(SpecialAttackStats back) //Back Special Attack
    {
		Debug.Log ("Backward_Special");
    }
    public virtual void DownSA(SpecialAttackStats down) //Down Special Attack
    {
		Debug.Log ("Down_Special");
    }
    public virtual void JumpSA(SpecialAttackStats jump) //Jump Special Attack
    {
		Debug.Log ("Jump_Special");
    }
    public virtual void BreakdownSA(SpecialAttackStats breakdown) //Breakdown
    {
		Debug.Log ("BreakDown");
    }
}
