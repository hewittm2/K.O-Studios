//Torrel L Ethan Q
//4/8/19
//Apollo Special Attacks
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolloSpecialAttacks : SpecialAttackTemplate
{
    //Custom Components
    private FighterClass self;
    private CharacterController moveCheck;
    private BaseMovement getGravity;
    private GameObject managerHold;

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
    public bool jumpSpecialActive = false;

    [Header("Breakdown Special Custom Variables")][Space(0.5f)]
    [Range(7,20)]
    public float breakdownMoveSpeed;
    public GameObject breakdownHitBox;
    private bool breakdownActive = false;
    private bool isFacingRight = false;

    [Header("Coup De Grace")] [Space(0.5f)]
    public int coupDamage = 10;
    [Tooltip("How fast the star moves (Start Speed)")] [Range(3, 10)]
    public float moveSpeed = 5f;
    [Tooltip("This value increases the rate at which it picks up speed ")][Range(10, 25)]
    public int speedIncreaseOT = 10;
    [Tooltip("How fast the ministars move")][Range(1, 25)]
    public float miniStarSpeed = 1f;
    private bool moveMini;
    private bool moveTowards;
    public GameObject theTrail;
    public GameObject coupExplosion;
    public GameObject coupObj;
    [SerializeField]private GameObject[] miniStars;
    public GameObject coupStar;

    [Header("Read Only - Value ignored.")][Space(0.5f)]
    [SerializeField] private FighterClass[] fightersC;
    [SerializeField] private GameObject[] enemies;

    private void Awake()
    {
        GetPrivateComponents();
        CheckForHitBoxErrors();
    }
    void GetPrivateComponents()
    {
        self = GetComponent<FighterClass>();
        moveCheck = GetComponent<CharacterController>();
        getGravity = GetComponent<BaseMovement>();
        forwardObj.GetComponent<ProjectileFighterReference>().fighter = self;
        forwardRigidbody = forwardObj.GetComponent<Rigidbody>();
    }
    void CheckForHitBoxErrors()
    {
        if (neutralHitBox == null)
            Debug.LogError("No Neutral HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
        if (backHitBox == null)
            Debug.LogError("No Back HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
        if (forwardObj == null)
            Debug.LogError("No Projectile Prefab Set! - Assign the Projectile Prefab - Stopping Special Attack.", gameObject);
        if (customJump == null)
            Debug.LogError("No Jump HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
        if (downHitBox == null)
            Debug.LogError("No Down HitBox Set! - Assign the Hitbox, should be childed under Apollo - Stopping Special Attack.", gameObject);
        if (breakdownHitBox == null)
            Debug.LogError("No Breakdown HitBox Set! - Assign the it, should be attached to Apollo - Stopping Special Attack.", gameObject);
        if (coupObj == null || coupStar == null)
            Debug.LogError("No Coup Object OR Coup Star Set! - Assign the variables, should be childed under Apollo or in the scene - Stopping Coup De Grace.", gameObject);

        SetHitBoxes();
    }
    void SetHitBoxes()
    {
        managerHold = new GameObject("Player: " + self.playerNumber + "'s" + " HitBoxHolder");
        forwardObj.transform.SetParent(managerHold.transform);
        coupObj.transform.SetParent(managerHold.transform);
        coupStar.transform.SetParent(managerHold.transform);
        miniStars[0] = coupObj.transform.GetChild(0).gameObject;
        miniStars[1] = coupObj.transform.GetChild(1).gameObject;
        miniStars[2] = coupObj.transform.GetChild(2).gameObject;
        miniStars[3] = coupObj.transform.GetChild(3).gameObject;
        miniStars[4] = coupObj.transform.GetChild(4).gameObject;
        miniStars[0].GetComponent<ProjectileFighterReference>().fighter = self;
        miniStars[1].GetComponent<ProjectileFighterReference>().fighter = self;
        miniStars[2].GetComponent<ProjectileFighterReference>().fighter = self;
        miniStars[3].GetComponent<ProjectileFighterReference>().fighter = self;
        miniStars[4].GetComponent<ProjectileFighterReference>().fighter = self;

        if (self.isActiveAndEnabled == false)
        {
            Debug.Log("off");
        }

    }
    void CoupEnemyCheck()
    {
        enemies = new GameObject[2];
        fightersC = FindObjectsOfType<FighterClass>();
        int holder = 0;
        foreach (FighterClass fighter in fightersC)
        {
            if (fighter.teamNumber != self.teamNumber)
            {
                enemies[holder] = fighter.gameObject;
                holder++;
            }
        }
    }
    void SetVars(SpecialAttacks _SetVar)
    {
        if (self.facingRight == true)
            self.output.knockBackDirection = new Vector3(_SetVar.knockback.x, _SetVar.knockback.y, _SetVar.knockback.z);
        else
            self.output.knockBackDirection = new Vector3(-_SetVar.knockback.x, _SetVar.knockback.y, _SetVar.knockback.z);
		self.output.meterGain = _SetVar.meterGain;
        self.output.knockBackForce = _SetVar.knockbackForce;
        self.output.attDam = _SetVar.damage;
		self.output.damageType = _SetVar.damageType;
		self.output.hitType = _SetVar.hitType;
    }
    public override void NeutralSA(SpecialAttacks neutral)
    {
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
		SetVars (specialAttackStats.SpecialForward);
        if (forwardObj.activeInHierarchy == false)
        {
            StartCoroutine(ForwardSAC(forward.startupTime, forward.activeTime));
        }
        else
            Debug.Log("Forward Special is still active! You can't start another one yet");
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
        SetVars(breakdown);
        StartCoroutine(BreakdownSAC(breakdown.activeTime, breakdown.startupTime));
    }
    IEnumerator BreakdownSAC(float active, float startup)
    {
        yield return new WaitForSeconds(startup);
        breakdownHitBox.SetActive(true);
        if (self.facingRight == true)
            isFacingRight = true;
        else
            isFacingRight = false;
        breakdownActive = true;
        yield return new WaitForSeconds(active);
        breakdownHitBox.SetActive(false);
        breakdownActive = false;
    }
    //---------------------------------------------------
    private void Update()
    {
        //Jump Special
        if (jumpSpecialActive && moveCheck.isGrounded)
        {
            jumpSpecialActive = false;
            customJump.SetActive(false);
            getGravity.gravity = -0.7f;
        }
        //Breakdown
        if (breakdownActive == true)
        {
            if(isFacingRight == true)
            transform.Translate(Vector3.right * Time.deltaTime * breakdownMoveSpeed,Space.World);
            else
                transform.Translate(Vector3.left * Time.deltaTime * breakdownMoveSpeed, Space.World);
        }
        //Coup De Grace
        if (moveMini == true)
        {
            MoveMiniStars();
        }
        if (moveTowards == true)
        {
            if (theTrail.activeInHierarchy == false)
            {
                theTrail.SetActive(true);
            }
            moveSpeed += Time.deltaTime * speedIncreaseOT;
            coupStar.transform.position = Vector3.MoveTowards(coupStar.transform.position, enemies[0].transform.position, 0.05f * moveSpeed);
            float dist = Vector3.Distance(coupStar.transform.position, enemies[0].transform.position);

            if (dist <= 0.06f)
            {
                theTrail.SetActive(false);
                coupStar.transform.GetChild(0).gameObject.SetActive(false);
                moveTowards = false;
                StartCoroutine(ExplosionWait());
            }
        }
    }
    //---------------------------------------------------
    public override void CoupDeGraceU(SpecialAttacks coup)
    {
        SetVars(coup);
        self.coupDeGraceActivated = true;
        coupObj.SetActive(true);
        miniStars[0].SetActive(true);
        miniStars[1].SetActive(true);
        miniStars[2].SetActive(true);
        miniStars[3].SetActive(true);
        miniStars[4].SetActive(true);
        moveMini = true;
        CoupEnemyCheck();
        StartCoroutine(CoupDeGraceUE());
    }
    void MoveMiniStars()
    {
        miniStars[0].transform.Translate(10 * Time.deltaTime * miniStarSpeed, 0, 0, Space.World);
        miniStars[3].transform.Translate(10 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[2].transform.Translate(0 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[4].transform.Translate(-10 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[1].transform.Translate(-10 * Time.deltaTime * miniStarSpeed, 0, 0, Space.World);
    }
    IEnumerator CoupDeGraceUE()
    {
        yield return new WaitForSeconds(1f);
        moveMini = false;
        miniStars[0].SetActive(false);
        miniStars[1].SetActive(false);
        miniStars[2].SetActive(false);
        miniStars[3].SetActive(false);
        miniStars[4].SetActive(false);
        coupObj.SetActive(false);
        coupStar.transform.position = new Vector3(transform.position.x,transform.position.y + 15, transform.position.z);
        coupStar.SetActive(true);
        for (int l = 0; l < 2; l++)
        {
            yield return new WaitForSeconds(1.5f);
            coupStar.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(1.5f);
        moveTowards = true;
    }
    IEnumerator ExplosionWait()
    {
        HitDetection doMajorDamage = enemies[0].transform.root.gameObject.GetComponent<HitDetection>();
        //HitDetection doMajorDamage2 = enemies[1].transform.root.gameObject.GetComponent<HitDetection>();
        self.output.attDam = coupDamage;
        doMajorDamage.ReceiveDamage(self.output);
      // doMajorDamage2.ReceiveDamage(self.output);
        coupExplosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        coupStar.gameObject.SetActive(false);
        ResetCoup();
    }

    void ResetCoup()
    {
        self.coupDeGraceActivated = false;
        coupExplosion.SetActive(false);
        coupStar.transform.GetChild(0).gameObject.SetActive(true);
        miniStars[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        miniStars[1].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -5);
        miniStars[2].transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z + 0);
        miniStars[3].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z + 5);
        miniStars[4].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z + -5);
    }
}
