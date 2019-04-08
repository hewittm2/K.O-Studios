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
    [Range(1f,20f)]
    public float projectileSpeed;
    [Range(3,5)]
    public float xSpawn;
    [Range(0.3f,0.7f)]
    public float ySpawn;
    [Tooltip("Z Spawn is Locked, Sorry!")]
    [Range(0,0)]
    public float zSpawn;
    private Vector3 spawn;
    private Rigidbody forwardRigidbody;

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


    [Header("Coup De Grace")][Space(0.5f)]
    public GameObject coupObj;
    public GameObject coupStar;

    private void Awake()
    {
        self = GetComponent<FighterClass>();
        moveCheck = GetComponent<CharacterController>();
        getGravity = GetComponent<BaseMovement>();
        forwardObj.GetComponent<ProjectileFighterReference>().fighter = self;
        forwardRigidbody = forwardObj.GetComponent<Rigidbody>();
    }
    void SetVars(SpecialAttacks _SetVar)
    {
        if (self.facingRight == true)
            self.output.knockBackDirection = new Vector3(_SetVar.knockback.x, _SetVar.knockback.y, _SetVar.knockback.z);
        else
            self.output.knockBackDirection = new Vector3(-_SetVar.knockback.x, _SetVar.knockback.y, _SetVar.knockback.z);

        self.output.knockBackForce = _SetVar.knockbackForce;
        self.output.attDam = _SetVar.damage;
		self.output.damageType = _SetVar.damageType;
		self.output.hitType = _SetVar.hitType;
    }
    public override void NeutralSA(SpecialAttacks neutral)
    {
        if (neutralHitBox == null)
        {
            Debug.LogError("No Neutral HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        SetVars(neutral);
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
        if (backHitBox == null)
        {
            Debug.LogError("No Back HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        SetVars(back);
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
		SetVars (specialAttackStats.SpecialForward);
        StartCoroutine(ForwardSAC(forward.startupTime,forward.activeTime));
    }
    IEnumerator ForwardSAC(float wait, float active)
    {
        if (self.facingRight == true)
            spawn = new Vector3(transform.position.x + xSpawn, transform.position.y - ySpawn, transform.position.z + zSpawn);
        else
            spawn = new Vector3(transform.position.x + -xSpawn, transform.position.y - ySpawn, transform.position.z + zSpawn);

        forwardObj.transform.position = spawn;
        yield return new WaitForSeconds(wait);
        forwardObj.SetActive(true);

        if (self.facingRight == true)
            forwardRigidbody.AddForce(projectileSpeed * 100, 0, 0);
        else
            forwardRigidbody.AddForce(-projectileSpeed * 100, 0, 0);

        yield return new WaitForSeconds(active);
        if (forwardObj.activeInHierarchy == true)
        {
            forwardObj.SetActive(false);
            forwardRigidbody.velocity = Vector3.zero;
        }
    }
    //---------------------------------------------------
    public override void JumpSA(SpecialAttacks jump)
    {
        if (customJump == null)
        {
            Debug.LogError("No Jump HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        if (transform.position.y > minimumHeightNeeded)
        {
            SetVars(jump);
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
        if (downHitBox == null)
        {
            Debug.LogError("No Down HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        SetVars(down);
        StartCoroutine(DownSAC(down.activeTime));
    }
    IEnumerator DownSAC(float active)
    {
        downHitBox.SetActive(true);
        yield return new WaitForSeconds(active);
        downHitBox.SetActive(false);
    }
    //---------------------------------------------------
    public override void BreakdownSA(SpecialAttacks breakdown)
    {
        if (breakdownHitBox == null)
        {
            Debug.LogError("No Breakdown HitBox Set! - Assign the it, should be attached to Apollo - Stopping Special Attack.", gameObject);
            return;
        }
        SetVars(breakdown);
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
    //---------------------------------------------------
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
    //---------------------------------------------------
    public override void CoupDeGraceU(SpecialAttacks coup)
    {
        if (coupObj == null || coupStar == null)
        {
            Debug.LogError("No Coup Object OR Coup Star Set! - Assign the variables, should be childed under Apollo or in the scene - Stopping Coup De Grace.", gameObject);
            return;
        }
        SetVars(coup);
        self.coupDeGraceActivated = true;
        coupObj.SetActive(true);
        coupStar.GetComponent<ApolloCoupDG>().selfCheck = self;
        StartCoroutine(CoupDeGraceUE());
    }
    IEnumerator CoupDeGraceUE()
    {
        yield return new WaitForSeconds(1f);
        coupStar.transform.position = new Vector3(transform.position.x, coupStar.transform.position.y, transform.position.z);
        coupStar.SetActive(true);
        for (int l = 0; l < 2; l++)
        {
            yield return new WaitForSeconds(1.5f);
            coupStar.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(1.5f);
        ApolloCoupDG activeMove = coupStar.GetComponent<ApolloCoupDG>();
        activeMove.moveTowards = true;
    }

    void ResetCoup()
    {

    }
}
