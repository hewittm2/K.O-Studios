//Torrel L Ethan Q
//5/5/19
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
    private CoupManager coupMang;

    [Header("Particle Color Change")]
    public ParticleSystem[] myParticleColors;
    public TrailRenderer[] myTrailColors;
    private Color myPColor;


    [Header("Forward Special Custom Variables")][Space(0.5f)]
    public GameObject forwardObj;
    [Range(1f,20f)]
    public float projectileSpeed;
    private Vector3 spawn;
    private Rigidbody forwardRigidbody;
    private bool isForwardActive = false;

    [Header("Back Special Custom Variables")][Space(0.5f)]
    public GameObject backHitBox;
    public GameObject backParticle;
    private bool isBackActive = false;

    [Header("Down Special Custom Variables")][Space(0.5f)]
    public GameObject downHitBox;
    public GameObject downParticle;

    [Header("Neutral Special Custom Variables")][Space(0.5f)]
    public GameObject neutralHitBox;
    private bool nCooldown = false;

    [Header("Jump Special Custom Variables")][Space(0.5f)]
    [Range(1.5f,1.5f)]
    public float downSpeed;
    [Range(0.5f, 0.5f)]
    public float upHeight;
    private bool goingUp = false;
    public GameObject customJump;
    private bool jumpSpecialActive = false;
    [Range(1,5)]
    public int jCooldown;
    private bool offCooldown = false;

    [Header("Breakdown Special Custom Variables")][Space(0.5f)]
    [Range(7,20)]
    public float breakdownMoveSpeed;
    public GameObject breakdownHitBox;
    private bool breakdownActive = false;
    private bool isFacingRight = false;

    [Header("Coup De Grace")] [Space(0.5f)]
    [Tooltip("How fast the star moves (Start Speed)")] [Range(3, 10)]
    public float moveSpeed = 5f;
    [Tooltip("This value increases the rate at which it picks up speed ")][Range(10, 25)]
    public int speedIncreaseOT = 10;
    private bool moveMini;
    private bool moveTowards;
    [Header("Mini Stars")]
    [Tooltip("How fast the ministars move")][Range(3, 7)]
    public float miniStarSpeed = 3f;
    public GameObject coupObj;
    public GameObject[] miniStars;
    [Header("Big Coup Star")]
    public GameObject coupStar;
    public GameObject theTrail;
    public GameObject coupExplosion;

    [Header("Read Only - Value ignored.")][Space(0.5f)]
    private List<FighterClass> theEnemies = new List<FighterClass>();
    private List<HitDetection> doMajorDamage = new List<HitDetection> ();

    private void Awake()
    {
        GetPrivateComponents();
        StartCoroutine(CheckForHitBoxErrors());
    }
    void GetPrivateComponents()
    {
        self = GetComponent<FighterClass>();
        moveCheck = GetComponent<CharacterController>();
        getGravity = GetComponent<BaseMovement>();
        forwardObj.GetComponent<ProjectileFighterReference>().fighter = self;
        forwardRigidbody = forwardObj.GetComponent<Rigidbody>();
        coupMang = FindObjectOfType<CoupManager>();
    }
    IEnumerator CheckForHitBoxErrors()
    {
        yield return new WaitForSeconds(0.2f);
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
        if (self.teamNumber == 1)
            myPColor = new Color(0, 0, 255);
        else
            myPColor = new Color(255, 0, 0);

        foreach (ParticleSystem p in myParticleColors)
        {
            ParticleSystem.MainModule newMain = p.main;
            newMain.startColor = myPColor;
        }
        foreach (TrailRenderer t in myTrailColors)
            t.startColor = myPColor;


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
        FighterClass[] fightersC = FindObjectsOfType<FighterClass>();
        foreach (FighterClass fighter in fightersC)
        {
            if (fighter.teamNumber != self.teamNumber)
            {
                theEnemies.Add(fighter);
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
    void SetVarsCleart()
    {
        self.output.meterGain = 0;
        self.output.hitType = FighterClass.HitType.Light;
        self.output.hitHeight = FighterClass.HitHeight.High;
        self.output.damageType = FighterClass.DamageType.Hit;
        self.output.knockBackDirection = new Vector3(0,0,0);
        self.output.knockBackForce = 0;
        self.output.attDam = 0;
    }
    void CharCheck(bool _canNowMove, bool _canNowAttack)
    {
        if (_canNowMove == false)
            self.canMove = false;
        else
            self.canMove = true;

        if (_canNowAttack == false)
            self.canAttack = false;
        else
            self.canAttack = true;
    }
    public override void NeutralSA(SpecialAttacks neutral)
    {
        if (nCooldown == false)
        {
            SetVars(neutral);
            StartCoroutine(NeutralSAC(neutral.startupTime, neutral.activeTime));
        }
        else
            Debug.Log("Neutral On Cooldown");
    }
    IEnumerator NeutralSAC(float wait, float active)
    {
        nCooldown = true;
        yield return new WaitForSeconds(wait);
        neutralHitBox.SetActive(true);
        specialAttackStats.SpecialNeutral.objects.SetActive(true);
        yield return new WaitForSeconds(active);
        neutralHitBox.SetActive(false);
        specialAttackStats.SpecialNeutral.objects.SetActive(false);
        nCooldown = false;
    }
    // --------------------------------------------------
    public override void BackSA(SpecialAttacks back)
    {
        SetVars(back);
        StartCoroutine(BackSAC(back.startupTime, back.activeTime));
    }
    IEnumerator BackSAC(float wait, float active)
    {
        isBackActive = true;
        backParticle.SetActive(true);
        yield return new WaitForSeconds(wait);
        backHitBox.SetActive(true);
        isBackActive = false;
        yield return new WaitForSeconds(active);
        backHitBox.SetActive(false);
        backParticle.SetActive(false);
   //     SetVarsClear();
    }
    //---------------------------------------------------
    public override void ForwardSA(SpecialAttacks forward)
    {
        if (isForwardActive == false)
        {
            isForwardActive = true;
            forwardObj.transform.GetChild(3).GetComponent<TrailRenderer>().Clear();
            SetVars(specialAttackStats.SpecialForward);
            StartCoroutine(ForwardSAC(forward.startupTime, forward.activeTime));
        }
        else
            Debug.Log("Forward Special is still active! You can't start another one yet");
    }
    IEnumerator ForwardSAC(float wait, float active)
    {
        if (self.facingRight == true)
            spawn = new Vector3(transform.position.x + 5, transform.position.y + 2 + 5, transform.position.z);
        else
            spawn = new Vector3(transform.position.x + -5, transform.position.y + 2 + 5, transform.position.z);

        forwardObj.transform.position = spawn;
        forwardObj.SetActive(true);
        yield return new WaitForSeconds(wait);


        if (self.facingRight == true)
            forwardRigidbody.AddForce(projectileSpeed * 100, 0, 0);
        else
            forwardRigidbody.AddForce(-projectileSpeed * 100, 0, 0);

        yield return new WaitForSeconds(active);
        forwardObj.SetActive(false);

        forwardRigidbody.velocity = Vector3.zero;
        isForwardActive = false;
        
    }
    //---------------------------------------------------
    public override void JumpSA(SpecialAttacks jump)
    {
        //if (offCooldown == false)
       // {
            offCooldown = true;
            //CharCheck(false, false);
            SetVars(jump);
            jumpSpecialActive = true;
            StartCoroutine(JumpSAC(jump.startupTime));
        //}
    }
    IEnumerator JumpSAC(float startup)
    {
        goingUp = true;
        getGravity.gravity = 0;
        specialAttackStats.SpecialJump.objects.SetActive(true);
        yield return new WaitForSeconds(startup);
        goingUp = false;
        yield return new WaitForSeconds(0.1f);
        getGravity.gravity = -downSpeed;
        customJump.SetActive(true);
    }
    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(jCooldown);
        offCooldown = false;
    }
    //---------------------------------------------------
    public override void DownSA(SpecialAttacks down)
    {
        SetVars(down);
        StartCoroutine(DownSAC(down.startupTime,down.activeTime));
    }
    IEnumerator DownSAC(float startup,float active)
    {
        downParticle.SetActive(true);
        yield return new WaitForSeconds(startup);
        downHitBox.SetActive(true);
        yield return new WaitForSeconds(active);
        downHitBox.SetActive(false);
        downParticle.SetActive(false);
    }
    //---------------------------------------------------
    public override void BreakdownSA(SpecialAttacks breakdown)
    {
        SetVars(breakdown);
        StartCoroutine(BreakdownSAC(breakdown.activeTime, breakdown.startupTime));
    }
    IEnumerator BreakdownSAC(float active, float startup)
    {
        specialAttackStats.SpecialBreakdown.objects.SetActive(true);
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
        specialAttackStats.SpecialBreakdown.objects.SetActive(false);
       // CharCheck(true, true);
    }
    //---------------------------------------------------
    private void Update()
    {
        //Back Special 
        if (backHitBox.activeInHierarchy == false && isBackActive == false)
            backParticle.SetActive(false);

        //Jump Special
        if (jumpSpecialActive && moveCheck.isGrounded)
        {
            jumpSpecialActive = false;
            customJump.SetActive(false);
            specialAttackStats.SpecialJump.objects.SetActive(false);
            getGravity.gravity = -0.7f;
            //CharCheck(true,true);
            //StartCoroutine(JumpCoolDown());
        }
        if(goingUp == true)
            transform.Translate(Vector3.up * Time.deltaTime * 40 * upHeight, Space.World);
        if (jumpSpecialActive == true)
           // CharCheck(false, false);

        //Breakdown
        if (breakdownActive == true)
        {
            if(isFacingRight == true)
            transform.Translate(Vector3.right * Time.deltaTime * breakdownMoveSpeed,Space.World);
            else
                transform.Translate(Vector3.left * Time.deltaTime * breakdownMoveSpeed, Space.World);

            //CharCheck(false, false);
        }

        if (breakdownActive == true && breakdownHitBox.activeInHierarchy == false)
        {
            breakdownActive = false;
          //  CharCheck(true, true);
            specialAttackStats.SpecialBreakdown.objects.SetActive(false);
        }
        //Coup De Grace (Part 1 Waiting, and checking if teammate wants to stop)
        if(self.coupDeGraceActivated == true)
            //CharCheck(false, false);

        //Coup De Grace (Part 2 // Activated // After both teammates inputted
        if (moveMini == true)
            MoveMiniStars();

        if (moveTowards == true)
        {
            moveSpeed += Time.deltaTime * speedIncreaseOT;
            coupStar.transform.position = Vector3.MoveTowards(coupStar.transform.position, theEnemies[0].gameObject.transform.position, 0.05f * moveSpeed);
            float dist = Vector3.Distance(coupStar.transform.position, theEnemies[0].gameObject.transform.position);

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

        StartCoroutine(CoupDeGraceUE(coup));
    }
    void MoveMiniStars()
    {
        miniStars[0].transform.Translate(10 * Time.deltaTime * miniStarSpeed, 0, 0, Space.World);
        miniStars[3].transform.Translate(10 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[2].transform.Translate(0 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[4].transform.Translate(-10 * Time.deltaTime * miniStarSpeed, 10 * Time.deltaTime * miniStarSpeed, 0, Space.World);
        miniStars[1].transform.Translate(-10 * Time.deltaTime * miniStarSpeed, 0, 0, Space.World);
    }
    IEnumerator CoupDeGraceUE(SpecialAttacks coup)
    {
        Debug.Log("Coup Wait");
        yield return new WaitForSeconds(0.1f);
        if (self.teamNumber == 1)
            coupMang.t1++;
        if (self.teamNumber == 2)
            coupMang.t2++;

        if (self.teamNumber == 1 && coupMang.t1 == 2)
        {
            self.anim.SetTrigger("Win Pose");
            StopCoroutine("CoupDeGraceUE");
        }

        if (self.teamNumber == 2 && coupMang.t2 == 2)
        {
            self.anim.SetTrigger("Win Pose");
            StopCoroutine("CoupDeGraceUE");
        }

        if (self.teamNumber == 1)
            yield return new WaitUntil(() => coupMang.t1CoupSuccessful == true);
        if (self.teamNumber == 2)
            yield return new WaitUntil(() => coupMang.t2CoupSuccessful == true);
        SetVars(coup);
        self.coupDeGraceActivated = true;
        self.anim.SetTrigger("Win Pose"); //Play Win Pose here??
        //Wait for teammate input here
        ActivateMiniStars();
        CoupEnemyCheck();
        yield return new WaitForSeconds(1f);
        StartCoroutine(PulseMiniStars());
        coupStar.transform.position = new Vector3(transform.position.x,transform.position.y + 20, transform.position.z);
        coupStar.SetActive(true);
        for (int l = 0; l < 2; l++)
        {
            yield return new WaitForSeconds(1.5f);
            coupStar.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(1.5f);
        theTrail.SetActive(true);
        moveTowards = true;
    }
    IEnumerator ExplosionWait()
    {
        coupExplosion.SetActive(true);
        foreach (FighterClass fighter in theEnemies)
        {
            doMajorDamage.Add(fighter.gameObject.transform.root.gameObject.GetComponent<HitDetection>());
        }

        self.output.attDam = theEnemies[0].totalHealth;
        if (theEnemies[0] != null)
            doMajorDamage[0].ReceiveDamage(self.output);
        if (theEnemies[1] != null)
            doMajorDamage[1].ReceiveDamage(self.output);

        yield return new WaitForSeconds(1f);
        coupStar.gameObject.SetActive(false);
        ResetCoup();
    }

    void ResetCoup()
    {
        self.coupDeGraceActivated = false;
        //CharCheck(true, true);
        coupExplosion.SetActive(false);
        coupStar.transform.GetChild(0).gameObject.SetActive(true);
        miniStars[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        miniStars[1].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -5);
        miniStars[2].transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z + 0);
        miniStars[3].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z + 5);
        miniStars[4].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z + -5);
    }

    void ActivateMiniStars()
    {
        ResetPositions();
        coupObj.SetActive(true);
        foreach (GameObject ms in miniStars)
        {
            ms.SetActive(true);
        }
        moveMini = true;
    }
    IEnumerator PulseMiniStars()
    {
        coupObj.SetActive(false);
        foreach (TrailRenderer t in myTrailColors)
            t.Clear();
        ResetPositions();
        coupObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        coupObj.SetActive(false);
        foreach (TrailRenderer t in myTrailColors)
            t.Clear();
        ResetPositions();
        self.output.damageType = FighterClass.DamageType.KnockDown;
        coupObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeActivateMiniStars();
    }
    void DeActivateMiniStars()
    {
        ResetPositions();
        moveMini = false;
        foreach (GameObject ms in miniStars)
        {
            ms.SetActive(false);
        }
        coupObj.SetActive(false);

    }
    void ResetPositions()
    {
        miniStars[0].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        miniStars[1].transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        miniStars[2].transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        miniStars[3].transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);
        miniStars[4].transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);
    }
}
