using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolloSpecialAttacks : SpecialAttackTemplate
{
    //Custom Components
    private FighterClass self;
    private CharacterController moveCheck;
    private BaseMovement getGravity;

    [Header("Forward Special Custom Variables")][Space(0.5f)]
    public GameObject forwardObj;
    private GameObject holder;

    [Header("Back Special Custom Variables")][Space(0.5f)]
    public GameObject backHitBox;
    public GameObject backParticle;

    [Header("Down Special Custom Variables")][Space(0.5f)]
    public GameObject downHitBox;

    [Header("Neutral Special Custom Variables")][Space(0.5f)]
    public GameObject neutralHitBox;

    [Header("Jump Special Custom Variables")][Space(0.5f)]
    [Range(0.5f,15f)]
    public float downSpeed;
    [Range(1f, 5f)]
    public float minimumHeightNeeded;
    public GameObject customJump;
    private bool jumpSpecialActive = false;

    [Header("Breakdown Special Custom Variables")][Space(0.5f)]
    [Range(7,20)]
    public float breakdownMoveSpeed;
    public GameObject breakdownHitBox;
    private bool breakdownActive = false;

    private void Awake()
    {
        self = GetComponent<FighterClass>();
        moveCheck = GetComponent<CharacterController>();
        getGravity = GetComponent<BaseMovement>();
    }
    void SetVars(SpecialAttacks _SetVar, string _HitType, string _DamageType)
    {
        self.output.knockBackDirection = _SetVar.knockback;
        self.output.knockBackForce = _SetVar.knockbackForce;
        self.output.attDam = _SetVar.damage;

        if (_HitType == "High")
            self.output.hitType = FighterClass.AttackStats.HitType.High;
        if (_HitType == "Mid")
            self.output.hitType = FighterClass.AttackStats.HitType.Mid;
        if (_HitType == "Low")
            self.output.hitType = FighterClass.AttackStats.HitType.Low;

        if (_DamageType == "Hit")
            self.output.damageType = FighterClass.AttackStats.DamageType.Hit;
        if (_DamageType == "Stun")
            self.output.damageType = FighterClass.AttackStats.DamageType.Stun;
        if (_DamageType == "KD")
            self.output.damageType = FighterClass.AttackStats.DamageType.KnockDown;
    }
    public override void NeutralSA(SpecialAttacks neutral)
    {
        if (neutralHitBox == null)
        {
            Debug.LogError("No Neutral HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        string _holder = "High";
        string _holder2 = "Hit";
        SetVars(neutral, _holder, _holder2);
        StartCoroutine(NeutralSAC(neutral.startupTime,neutral.activeTime));
    }
    IEnumerator NeutralSAC(float wait, float active)
    {
        yield return new WaitForSeconds(wait);
        neutralHitBox.SetActive(true);
        yield return new WaitForSeconds(active);
        neutralHitBox.SetActive(false);
    }
    // --------------------------------------------------
    public override void BackSA(SpecialAttacks back)
    {
        string _holder = "High";
        string _holder2 = "KD";
        SetVars(back, _holder, _holder2);
        StartCoroutine(BackSAC(back.startupTime, back.activeTime));
    }
    IEnumerator BackSAC(float wait, float active)
    {
        backParticle.SetActive(true);
        yield return new WaitForSeconds(wait);
        backParticle.SetActive(false);
        backHitBox.SetActive(true);
        yield return new WaitForSeconds(active);
        backHitBox.SetActive(false);
    }
    //---------------------------------------------------
    public override void ForwardSA(SpecialAttacks forward)
    {
        if (forwardObj == null)
        {
            Debug.LogError("No Projectile Prefab Set! - Assign the Projectile Prefab - Stopping Special Attack.", gameObject);
            return;
        }
        StartCoroutine(ForwardSAC(forward.startupTime,forward.activeTime));
    }
    IEnumerator ForwardSAC(float wait, float active)
    {
        Vector3 spawn = new Vector3(transform.position.x + 4, transform.position.y - 0.5f, transform.position.z);
        holder = Instantiate(forwardObj, spawn, Quaternion.identity);
        holder.SetActive(false);
        yield return new WaitForSeconds(wait);
        holder.SetActive(true);
        Rigidbody move = holder.GetComponent<Rigidbody>();
        move.AddForce(400, 0, 0);
        yield return new WaitForSeconds(active);
        Destroy(holder);
    }
    //---------------------------------------------------
    public override void JumpSA(SpecialAttacks jump)
    {
        if (transform.position.y > minimumHeightNeeded)
        {
            string _holder = "High";
            string _holder2 = "Hit";
            SetVars(jump, _holder, _holder2);
            jumpSpecialActive = true;
            getGravity.gravity = -downSpeed;
            customJump.SetActive(true);
        }
        else
        {
            Debug.Log("You're too low to activiate this special attack",gameObject);
        }
    }
    //---------------------------------------------------
    public override void DownSA(SpecialAttacks down)
    {
        string _holder = "High";
        string _holder2 = "Hit";
        SetVars(down, _holder, _holder2);
      //  GameObject Go = Instantiate(down.partEffect,transform.position,Quaternion.identity);
        StartCoroutine(DownSAC(down.activeTime));
    }
    IEnumerator DownSAC(float active)
    {
        downHitBox.SetActive(true);
        yield return new WaitForSeconds(active);
        downHitBox.SetActive(false);
       // Destroy(go);
    }
    //---------------------------------------------------
    public override void BreakdownSA(SpecialAttacks breakdown)
    {
        if (breakdownHitBox == null)
        {
            Debug.LogError("No Breakdown HitBox Set! - Assign the it, should be attached to Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        string _holder = "High";
        string _holder2 = "KD";
        SetVars(breakdown, _holder, _holder2);
        StartCoroutine(BreakdownSAC(breakdown.activeTime, breakdown.startupTime));
    }
    IEnumerator BreakdownSAC(float active, float startup)
    {
        yield return new WaitForSeconds(startup);
        breakdownHitBox.SetActive(true);
        breakdownActive = true;
        yield return new WaitForSeconds(active);
        breakdownHitBox.SetActive(false);
        breakdownActive = false;
    }

    private void Update()
    {
        if (jumpSpecialActive && moveCheck.isGrounded)
        {
            jumpSpecialActive = false;
            customJump.SetActive(false);
            getGravity.gravity = -0.7f;
        }
        if (breakdownActive == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * breakdownMoveSpeed);
        }
    }
}
