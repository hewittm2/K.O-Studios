using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ryan Van Dusen, Chris B.
// 4/7/2019

public class MakoaSpecialAttacks : SpecialAttackTemplate {

    #region general Vars
    [Header("General Custom Variables")]
    [Space(0.5f)]
  
    public BaseMovement opponent;
    private BaseMovement movement;
    private HitDetection hitDetection;
    private FighterClass fighterClass;

    private GameObject enemy;
    private FighterClass self;
    #endregion

    #region Neutral Special Variables
    [Header("Neutral Custom Variables")]
    [Space(0.5f)]
 
    public float damageBuff = 1.1f;
    public int maxEmpowerTime;

    private float currentEmpowerTime = 0;
    private ParticleSystem empoweredParticle;

    #endregion

    #region Back Special Variables
    [Header("Back Custom Variables")]
    [Space(0.5f)]
    public int framesUsed = 60;
    public float throwSpeed = 20 ;
    public float rotationSpeed = 500;

   
    private GameObject clone;
    private Vector3 spawnPosition;
    private Vector3 rotation;
    private float throwTime;
    private bool throwing = false;
    #endregion

    #region ForwardSpecialVars
    [Header("Forward Custom Variables")]
    [Space(0.5f)]
    public float breathWaitTime = 2f;
    public float fireDamage;
    public float fireBreathCooldown = 9f;
    public float breathCooldown = 0f;
    private ParticleSystem coneOfFire;
   

    private FireScript fireCone;

    #endregion

    #region Jump Special Variables
    [Header("Jump Custom Variables")]
    [Space(0.5f)]
    private ParticleSystem fireSpitParticles;
    private FireSpitTracking fireSpitTracking;


    #endregion

    #region Down Special Variables
    [Header("Down Custom Variables")]
    [Space(0.5f)]

    public float fireYSpawn;
    public float spinTimeout = 1;
    public float spinSpeed = 300;
    public GameObject spinObject;

    private GameObject playerLocation;
    private GameObject spinClone;
    private GameObject fire;
    private bool spawned = false;
    private bool fireSpawned = false;
    private FireScript groundFire;
    private float fireCountDown;

    #endregion

    #region BreakDown Variables
    [Header("Breakdown Custom Variables")]
    [Space(0.5f)]
    public GameObject combinedKnives;
    public GameObject leftFireKnife;
    public GameObject rightFireKnife;
    public GameObject rotationPoint;

    public float breakdownKnifeSpeed;
    private float maxRotationDegrees = 720;
    private float currentRotationDegrees = 0;
    private bool rotateKnives;

    #endregion

    #region coupdegrace Variables
    [Header("Coup de grace Custom Variables")]
    [Space(0.5f)]
    private GameObject volcano;
    private Transform particles;
    private GameObject volcanoClone;
    private GameObject enemy1;
    private GameObject enemy2;
    private bool player1;
    private bool player2;
    private bool raise;
    private float volcanoCounter;


    #endregion

    private void Awake()
    {
        self = GetComponent<FighterClass>();
    }


    void Start()
    {
        
        playerLocation = this.gameObject;
        empoweredParticle = specialAttackStats.SpecialNeutral.partEffect;
        fireSpitParticles = specialAttackStats.SpecialJump.partEffect;
        coneOfFire = specialAttackStats.SpecialForward.partEffect;
        //fireSpitTracking = fireSpitParticles.GetComponent<FireSpitTracking>();
        fireCone = coneOfFire.GetComponent<FireScript>();
        movement = GetComponent<BaseMovement>();

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

    void Update()
    {

       
        #region Up Special
        // Checks for Damage to be done with the Up/Jump special attack
        if (fireSpitTracking != null)
        {
            if (fireSpitTracking.fireSpitHit)
            {
                Debug.Log("FIRE Spit Hit!");

                // If the knockdown is true and damage is not true from the particle effect, Knockdown enemy
                if (fireSpitTracking.knockdown && fireSpitTracking.doFireSpitDmg == false)
                {
                    // === Add code for knocking down enemy. Waiting to hear back from Ethan ===\\
                    Debug.Log("Knocked Down Enemy");
                    fireSpitTracking.knockdown = false;
                }

                // If the knockdown is true and damage is also true from the particle effect, Knockdown enemy, and do damage
                else if (fireSpitTracking.knockdown && fireSpitTracking.doFireSpitDmg)
                {
                    Debug.Log("Knocked Down and Damage Dealt");
                    // === Add code for knocking down enemy. Waiting to hear back from Ethan ===\\
                    fireSpitTracking.knockdown = false;
                    fireSpitTracking.doFireSpitDmg = false;
                }

                // If the enemy is blocking, knock them back without doing damage
                else if (fireSpitTracking.knockback)
                {
                    Debug.Log("Enemy Blocked. Push them back");
                    specialAttackStats.SpecialJump.knockbackForce = 5f;
                    fireSpitTracking.knockback = false;
                }

                // Enemy is on the ground and fire hit them, do damage
                else
                {
                    Debug.Log("Enemy Down. Damage Him");
                    // Add code for ranged attack damage. Need to talk to Ethan/Torrell
                    fireSpitTracking.doFireSpitDmg = false;
                }
            }
        }
        #endregion

        #region Down Special
        //checks if the staff spin spawned 
        if (spawned == true)
        {
            //timeout
            spinTimeout = spinTimeout - Time.deltaTime;
            if (spinTimeout <= 0)
            {
                //destorys
                Destroy(spinClone);
                spinObject.SetActive(true);
                spawned = false;
            }
        }
        
        //checks if the fire is there
        if(fireSpawned == true)
        {
            //timeout
            fireCountDown = fireCountDown - Time.deltaTime;
            if (fireCountDown <= 0)
            {
                //destroys
                Destroy(fire);
                fireSpawned = false;
            }
        }

        if (spinClone != null)
        {
            //spins
            spinClone.transform.Rotate(-spinSpeed * Time.deltaTime, 0, 0);
        }

        //fire damage stuff
        if (fire != null)
        {
            groundFire = fire.GetComponent<FireScript>();

            if (groundFire.doFireDmg)
            {
                //DO FIRE DAMAGE
                groundFire.doFireDmg = false;
            }
        }

        if (spinClone != null)
        {
            spinClone.tag = "attack1";
            if (spinClone.GetComponent<TriggerCheck>().opponentMove != null)
            {
                opponent = spinClone.GetComponent<TriggerCheck>().opponentMove;
            }
           
            if (spinClone.GetComponent<TriggerCheck>().opponent != null)
            {
                if (opponent.GetComponent<FighterClass>().canRecieveDamage != false)
                {
                    if (opponent.character.isGrounded == false)
                    {
                        if (opponent.GetComponent<FighterClass>().blocking != true)
                        {
                            if (spinClone.GetComponent<ProjectileFighterReference>().hit)
                            {
                                //DAMAGE 
                            }
                        }
                    }
                }
                if (opponent.GetComponent<FighterClass>().blocking == true)
                {
                    //Knockback
                }
            }
        
        }
        #endregion

        #region back special
        if (clone != null)
        {
         
            GetComponent<HitDetection>().specialBackward = clone;
         
            if (opponent != null)
            {
              
                if (opponent.GetComponent<FighterClass>().canRecieveDamage == true)
                    if (opponent.character.isGrounded == false)
                    {
                        if(clone.GetComponent<ProjectileFighterReference>().hit)
                        {
                            //KNOCKBACK HERE
                            Destroy(clone);
                        }

                    }
                else
                    {
                        if (clone.GetComponent<ProjectileFighterReference>().hit)
                        {
                            Destroy(clone);
                            //DEAL DAMAGE HERE
                        }
                    }

                if (opponent.GetComponent<FighterClass>().blocking == true)
                {
                    //KNOCKBACK

                    //moves back less then if not block
                    
                    //opponent.GetComponent<FighterClass>().damage =  damage - opponent.GetComponent<FighterClass>().defValue;
                    /*opponent.transform.Translate(new Vector3(-1, 0, 0));*/
                }
            }
        }
        if(clone == null && GetComponent<HitDetection>().specialBackward != null)
        {
            GetComponent<HitDetection>().specialBackward = null;
        }

        if (throwing == true && clone != null)
        {

            //rotates clone
            clone.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            //starts a counter
            throwTime = throwTime + Time.deltaTime;
            //moves clone
            clone.transform.position += (transform.forward * throwSpeed) * Time.deltaTime;
        }


        #endregion

        #region Breakdown
        if (rotateKnives)
        {
            enemy = GetComponent<FighterClass>().lockOnTarget;
            opponent = enemy.GetComponent<BaseMovement>();
            fighterClass = enemy.GetComponent<FighterClass>();
            float rotation = breakdownKnifeSpeed * Time.deltaTime;
            currentRotationDegrees -= rotation;

            if(currentRotationDegrees <= 0)
            {
                rotateKnives = false;
                combinedKnives.SetActive(false);
                leftFireKnife.SetActive(true);
                rightFireKnife.SetActive(true);
                return;
            }



            // ===== If Hit Detected ===== \\
            // If the enemy is lying on the ground, do damage
            // ========== Temporary until we have a knocked down variable ========= \\
            
            if (opponent.character.isGrounded)
            {
                //damage
                //specialAttackVars.damage = specialAttackStats.SpecialBreakdown.damage;
                Debug.Log("Enemy is lying down(suposedly), do damage");
            }
            //if (fighterClass.target.GetComponent<BaseMovement>().character.isGrounded)
            //{
            //    specialAttackVars.damage = specialAttackStats.SpecialBreakdown.damage;
            //    Debug.Log("Enemy is lying down(suposedly), do damage");
            //}


            // If the enemy is blocking, knock them back half as much without doing damage
            if(fighterClass.blocking)
            //if (fighterClass.target.GetComponent<FighterClass>().blocking)
            {
                //damage
                //specialAttackVars.knockback = specialAttackStats.SpecialBreakdown.knockback / 2;
                Debug.Log("Enemy is blocking, knock them back at half knockback");
            }

            // If the enemy isn't blocking, they get hit full force
            else
            {
                //damage
                //specialAttackVars.damage = specialAttackStats.SpecialBreakdown.damage;
                Debug.Log("Enemy is not blocking, do full damage");
            }

            // If the enemy is in the air, knock them back without doing damage
           
            if (!opponent.character.isGrounded)
            //if (!fighterClass.target.GetComponent<BaseMovement>().character.isGrounded)
            {
                //knockback
                //specialAttackVars.knockback = specialAttackStats.SpecialBreakdown.knockback;
                Debug.Log("Enemy is in the air, knoc them back and down");
            }

            rotationPoint.transform.Rotate(0, rotation, 0);

        }
        #endregion

        #region Foward Special
        if(breathCooldown <= 0)
        {
         
            GetComponent<FighterClass>().canMove = true;
            GetComponent<FighterClass>().canAttack = true;


        }
        if (breathCooldown > 0)
        {

   
            breathCooldown -= Time.deltaTime;
          
            if (fireCone.doFireDmg)
            {
                //DO DAMAGE
                fireCone.doFireDmg = false;

                // Add code for ranged damage. Waiting on Ethan/Torrell for more info
            }
        }
    
        #endregion

        #region Neutral Special
        //once timer is as big as value set in inspector
        if (currentEmpowerTime >= maxEmpowerTime)
        {
            //takes away damage buff
                //DAMAGE BUFF
            //specialAttackVars.damage = specialAttackVars.damage / damageBuff;
            //particles end
            empoweredParticle.gameObject.SetActive(false);
            //resets timer
            currentEmpowerTime = 0;
        }
        #endregion

        #region Coup De Grace
        if (raise == true)
        {
        
            volcanoClone.transform.Translate(0, 10 * Time.deltaTime, 0);
            
            if(volcanoClone.transform.position.y > -5)
            {
                particles.gameObject.SetActive(true);
                raise = false;
               //kill enemy?
            }


        }
        #endregion

        #region inputs
        // ===================================
        // ===== REMOVE WHEN IMPLEMENTED =====
        // ===================================
        //if (Input.GetButtonDown("B_1"))
        //{
        //    CoupDeGraceU(specialAttackStats.CoupDeGrace);
           
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    BreakdownSA(specialAttackStats.SpecialBreakdown);
        //}
        //if (Input.GetKeyUp(KeyCode.B))
        //{
        //    BackSA(specialAttackStats.SpecialBack);
        //}
        //if (Input.GetButtonDown("A_1"))
        //{
        //    NeutralSA(specialAttackStats.SpecialNeutral);
        //}
        //// ===================================
        //// ===================================
        //// ===================================
        #endregion
    }
    #region NeutralSA
    public override void NeutralSA(SpecialAttacks neutral)
    {
        SetVars(neutral);
        //checks to see if empowered or not
        if (currentEmpowerTime == 0)
        {
            //buffs damage (value set in inspector)
            //DAMAGE BUFF 
            //specialAttackVars.damage = specialAttackVars.damage * damageBuff;
            //starts a timer
            currentEmpowerTime = currentEmpowerTime + Time.deltaTime;
            //particles
            empoweredParticle.gameObject.SetActive(true);
        }

    }
    #endregion

    #region Back SA
    public override void BackSA(SpecialAttacks back)
    {
        SetVars(back);
        if (throwing != true)
        {
            //spawns a clone of the thrownobject gameobject
            clone = Instantiate(specialAttackStats.SpecialBack.objects);
            //sets clone position to orginal position
            clone.transform.position = specialAttackStats.SpecialBack.objects.transform.position;
            clone.transform.Translate(0, 0, -1f);
            //sets the orginal gameobject to not active (so it appears the object is thrown)
            specialAttackStats.SpecialBack.objects.SetActive(false);
            //sets clone's own scale and angles since the orginal is parented to the model
            clone.transform.localScale = new Vector3(0.5f, 5, 0.5f);
            clone.transform.eulerAngles = new Vector3(0, 0, 0);
            throwing = true;
            StartCoroutine(ThrownReturn());
        }
        

    }
    #endregion

    #region Forward SA
    public override void ForwardSA(SpecialAttacks forward)
    {

        GetComponent<FighterClass>().canMove = false;
        GetComponent<FighterClass>().canAttack = false;
        breathCooldown = fireBreathCooldown;
        SetVars(forward);

        StartCoroutine("BreathFire", breathWaitTime);
    }
    #endregion

    #region Jump SA
    public override void JumpSA(SpecialAttacks jump)
    {
        SetVars(jump);
        if (!movement.character.isGrounded)
        {
            //fireSpitTracking.fireSpitHit = false;
            fireSpitParticles.Play();

        }
    }
    #endregion

    #region Down SA
    public override void DownSA(SpecialAttacks down)
    {
        SetVars(down);

        //check statement
        if (spawned == false)
        {
            //resets the timer
            spinTimeout = 1;
            //checks if fire is there or not
            if (fire != null)
            {
                //destroys and restarts timer
                Destroy(fire);
                fireCountDown = 5;
            }
            //spawns fire
           
            fire = Instantiate(specialAttackStats.SpecialDown.objects);
            fireCountDown = 5;
            //spawns spinning object
            spinClone = Instantiate(spinObject, gameObject.transform.parent);
            //spinClone.SetActive(true);
            spinObject.SetActive(false);
            //translates to positions (most should be able to me removed after animations are IMPLEMENTED
            spinClone.transform.position = playerLocation.transform.position;
            spinClone.transform.Translate(new Vector3(5, 0, 0));
            spinClone.transform.eulerAngles = new Vector3(0, 0, 0);
            spinClone.transform.localScale = new Vector3(0.5f, 5, 0.5f);
            fire.transform.position = new Vector3(spinClone.transform.position.x, fireYSpawn, 0);
            spawned = true;
            fireSpawned = true;
          


        }


    }
    #endregion

    #region Breakdown SA
    public override void BreakdownSA(SpecialAttacks breakdown)
    {
        SetVars(breakdown);
        //  === If cooldown not reset, return out === \\
        if (combinedKnives.activeInHierarchy == true)
            return;

        else
        {
            currentRotationDegrees = maxRotationDegrees;
            // === Disable each knife / enable Combined knife === \\
            leftFireKnife.SetActive(false);
            rightFireKnife.SetActive(false);
            combinedKnives.SetActive(true);

            // === Set variables in Special Attack for easy access/calling === \\

            //Startup time and stun time?
            //specialAttackVars.startUpTime = specialAttackStats.SpecialBreakdown.startUpTime;
            //specialAttackVars.stunTime = specialAttackStats.SpecialBreakdown.stunTime;

            // ===== ATTACK AND MOVEMENT WILL BE HANDLED WITH ANIMATIONS ===== \\
            // ===== Hit Detection will be placed on combined fire knife for attack tracking ===== \\\

            // === Temporary "Animation" of knives below should be removed later === \\


            rotateKnives = true;
        }
    }
    #endregion

    #region Coup De Grace
    public override void CoupDeGraceU(SpecialAttacks coup)
    {
        SetVars(coup);
        //if (this.gameObject.tag == "Player1")
        //{
        //    player1 = true;
        //}
        //if (this.gameObject.tag == "Player2")
        //{
        //    player2 = true;
        //}
        //if(player1 == true && player2 == true)
        //{
        GetComponent<BaseMovement>().ResetMovement();
        GetComponent<BaseMovement>().enabled = false;

        volcanoClone = Instantiate(specialAttackStats.CoupDeGrace.objects);
        particles = volcanoClone.transform.Find("Effect19");
        particles.gameObject.SetActive(false);

        raise = true;

        volcanoClone.transform.position = new Vector3(17, -25.5f, 0);
        if (gameObject.layer == 10)
        {
            if (GetComponent<FighterClass>().lockOnTargets[0] != null)
            {
                enemy1 = GetComponent<FighterClass>().lockOnTargets[0];
                enemy1.GetComponent<FighterClass>().currentHealth = 0;
            }
            if (GetComponent<FighterClass>().lockOnTargets[1] != null)
            {
                enemy2 = GetComponent<FighterClass>().lockOnTargets[1];
                enemy2.GetComponent<FighterClass>().currentHealth = 0;
            }
        }
        if (gameObject.layer == 9)
        {
            if (GetComponent<FighterClass>().lockOnTargets[0] != null)
            {
                enemy1 = GetComponent<FighterClass>().lockOnTargets[0];
                enemy1.GetComponent<FighterClass>().currentHealth = 0;
            }
            if (GetComponent<FighterClass>().lockOnTargets[1] != null)
            {
                enemy2 = GetComponent<FighterClass>().lockOnTargets[1];
                enemy2.GetComponent<FighterClass>().currentHealth = 0;
            }
        }
    }
    #endregion

    #region corroutines
    IEnumerator ThrownReturn()
    {
        for (int i = 0; i < framesUsed; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        throwing = false;
        throwTime = 0;
        Destroy(clone);
        specialAttackStats.SpecialBack.objects.SetActive(true);
    }

    IEnumerator WaitTime(float wait)
    {

        yield return new WaitForSeconds(wait);
    }

    IEnumerator BreathFire(float wait)
    {
     
        yield return new WaitForSeconds(wait);

        fireCone.fireHit = false;
        coneOfFire.Play();
    }
    #endregion


}
