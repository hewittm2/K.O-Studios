using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ryan Van Dusen, Chris B.
// 3/10/2019

public class MakoaSpecialAttacks : SpecialAttackTemplate {
    #region general Vars
    private BaseMovement movement;
    private HitDetection hitDetection;
    private FighterClass fighterClass;
    public BaseMovement opponent;
    #endregion

    #region Neutral Special Variables

    public ParticleSystem empoweredParticle;
    public float damageBuff = 1.1f;
    public int maxEmpowerTime;
    private float currentEmpowerTime = 0;

    #endregion

    #region Back Special Variables

    public GameObject thrownObject;
    private GameObject clone;
    private Vector3 spawnPosition;

    public float throwSpeed;
    public float totalThrowTime;
    public float rotationSpeed;

    private Vector3 rotation;
    private float throwTime;
    private bool throwing = false;




    #endregion

    #region ForwardSpecialVars

    private ParticleSystem coneOfFire;
    public float breathWaitTime = 2f;
    public float fireDamage;
    public float fireBreathCooldown = 9f;
    private float breathCooldown = 0f;
    private FireScript fireCone;

    #endregion

    #region Jump Special Variables

    private ParticleSystem fireSpitParticles;
    private FireSpitTracking fireSpitTracking;


    #endregion

    #region Down Special Variables
    private GameObject spinClone;
    private GameObject fire;
    public float spinTimeout = 1;
    public float spinSpeed;
    public GameObject spinObject;
    public GameObject Player;
    public GameObject box;
    private bool spawned = false;
    private bool fireSpawned = false;
    private FireScript groundFire;
    private float fireCountDown;
    public float downDamage;
    #endregion

    #region BreakDown Variables

    public GameObject combinedKnives;
    public GameObject leftFireKnife;
    public GameObject rightFireKnife;
    public GameObject rotationPoint;

    public float breakdownKnifeSpeed;
    private float maxRotationDegrees = 720;
    private float currentRotationDegrees = 0;
    private bool rotateKnives;

    #endregion


    void Start()
    {
        fireSpitParticles = specialAttackStats.SpecialJump.partEffect;
        coneOfFire = specialAttackStats.SpecialForward.partEffect;
        fireSpitTracking = fireSpitParticles.GetComponent<FireSpitTracking>();
        fireCone = coneOfFire.GetComponent<FireScript>();
        movement = GetComponent<BaseMovement>();
    }

    void Update()
    {

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
                Debug.Log("THE FLOOR IS LAVA");
                groundFire.doFireDmg = false;


            }
        }

        if (spinClone != null)
        {
            if (spinClone.GetComponent<TriggerCheck>().opponent != null)
            {
                if (opponent.character.isGrounded == false)
                {
                    if (opponent.GetComponent<FighterClass>().canRecieveDamage != false)
                        opponent.GetComponent<FighterClass>().canRecieveDamage = false;

                }
                else
                {
                    opponent.GetComponent<FighterClass>().canRecieveDamage = true;
                    if (opponent.GetComponent<FighterClass>().blocking == true)
                    {
                        opponent.GetComponent<FighterClass>().damage = downDamage - opponent.GetComponent<FighterClass>().defValue;
                        opponent.transform.Translate(new Vector3(-2, 0, 0));
                    }
                    else
                    {
                        opponent.GetComponent<FighterClass>().damage = downDamage;
                    }
                }
            }
        
        }

        if (clone != null)
        {
            if(opponent.GetComponent<FighterClass>().canRecieveDamage != false)
            if (opponent.character.isGrounded == false)
            {
                opponent.character.transform.Translate(new Vector3(-2, 0, 0));
            }
         
            if (opponent.GetComponent<FighterClass>().blocking == true)
            {
                //moves back less then if not block
                opponent.GetComponent<FighterClass>().damage = downDamage - opponent.GetComponent<FighterClass>().defValue;
                opponent.transform.Translate(new Vector3(-1, 0, 0));
            }
            else
            {
                opponent.GetComponent<FighterClass>().damage = downDamage;
            }
        }


        // ===================================
        // ===== REMOVE WHEN IMPLEMENTED =====
        // ===================================
        //if (Input.GetButtonDown("B_1"))
        //{
        //    DownSA(specialAttackStats.SpecialDown);
        //}
        if (Input.GetKeyUp(KeyCode.A))
        {
            BreakdownSA(specialAttackStats.SpecialBreakdown);
        }
        //if(Input.GetButtonDown("B_1"))
        //{
        //    BackSA(specialAttackStats.SpecialBack);
        //}
        //if(Input.GetButtonDown("A_1"))
        //{
        //    NeutralSA(specialAttackStats.SpecialNeutral);
        //}
        // ===================================
        // ===================================
        // ===================================

        if(rotateKnives)
        {
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
            if(fighterClass.target.GetComponent<BaseMovement>().character.isGrounded)
            {
                specialAttackVars.damage = specialAttackStats.SpecialBreakdown.damage;
                Debug.Log("Enemy is lying down(suposedly), do damage");
            }


            // If the enemy is blocking, knock them back half as much without doing damage
            if (fighterClass.target.GetComponent<FighterClass>().blocking)
            {
                specialAttackVars.knockback = specialAttackStats.SpecialBreakdown.knockback / 2;
                Debug.Log("Enemy is blocking, knock them back at half knockback");
            }

            // If the enemy isn't blocking, they get hit full force
            else
            {
                specialAttackVars.damage = specialAttackStats.SpecialBreakdown.damage;
                Debug.Log("Enemy is not blocking, do full damage");
            }

            // If the enemy is in the air, knock them back without doing damage
            if (!fighterClass.target.GetComponent<BaseMovement>().character.isGrounded)
            {
                specialAttackVars.knockback = specialAttackStats.SpecialBreakdown.knockback;
                Debug.Log("Enemy is in the air, knoc them back and down");
            }

            rotationPoint.transform.Rotate(0, rotation, 0);

        }

        if (breathCooldown > 0)
        {
            breathCooldown -= Time.deltaTime;

            if (fireCone.doFireDmg)
            {
                Debug.Log("OW! FIRE DAMAGE!");
                fireCone.doFireDmg = false;
                // Add code for ranged damage. Waiting on Ethan/Torrell for more info
            }
        }
   

        if (throwing == true)
        {
            //rotates clone
            clone.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            //starts a counter
            throwTime = throwTime + Time.deltaTime;
            //moves clone
            clone.transform.position += (transform.forward * throwSpeed) * Time.deltaTime;
       
       
        }
  
        //once timer is as big as value set in inspector
        if (currentEmpowerTime >= maxEmpowerTime)
        {
            //takes away damage buff
            specialAttackVars.damage = specialAttackVars.damage / damageBuff;
            //particles end
            empoweredParticle.gameObject.SetActive(false);
            //resets timer
            currentEmpowerTime = 0;
        }
    }

    public override void NeutralSA(SpecialAttacks neutral)
    {
        //checks to see if empowered or not
        if (currentEmpowerTime == 0)
        {
            //buffs damage (value set in inspector)
            specialAttackVars.damage = specialAttackVars.damage * damageBuff;
            //starts a timer
            currentEmpowerTime = currentEmpowerTime + Time.deltaTime;
            //particles
            empoweredParticle.gameObject.SetActive(true);
        }

    }
    public override void BackSA(SpecialAttacks back)
    {
        if (throwing != true)
        {
            //spawns a clone of the thrownobject gameobject
            clone = Instantiate(thrownObject);
            //sets clone position to orginal position
            clone.transform.position = thrownObject.transform.position;
            //sets the orginal gameobject to not active (so it appears the object is thrown)
            thrownObject.SetActive(false);
            //sets clone's own scale and angles since the orginal is parented to the model
            clone.transform.localScale = new Vector3(0.5f, 5, 0.5f);
            clone.transform.eulerAngles = new Vector3(0, 0, 0);
            throwing = true;
            StartCoroutine(ThrownReturn());
        }

    }
    public override void ForwardSA(SpecialAttacks forward)
    {
        if(breathCooldown <= 0)
        {
            StartCoroutine("BreathFire", breathWaitTime);
        }
    }
    public override void JumpSA(SpecialAttacks jump)
    {
        if (!movement.character.isGrounded)
        {
            fireSpitTracking.fireSpitHit = false;
            fireSpitParticles.Play();

        }
    }
    public override void DownSA(SpecialAttacks down)
    {

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
            fire = Instantiate(box);
            fireCountDown = 5;
            //spawns spinning object
            spinClone = Instantiate(spinObject, gameObject.transform.parent);
            spinObject.SetActive(false);
            //translates to positions (most should be able to me removed after animations are IMPLEMENTED
            spinClone.transform.position = Player.transform.position;
            spinClone.transform.Translate(new Vector3(5, 0, 0));
            spinClone.transform.eulerAngles = new Vector3(0, 0, 0);
            spinClone.transform.localScale = new Vector3(0.5f, 5, 0.5f);
            fire.transform.position = new Vector3(spinClone.transform.position.x, 0, 0);
            spawned = true;
            fireSpawned = true;


        }


    }

    public override void BreakdownSA(SpecialAttacks breakdown)
    {
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
            specialAttackVars.startUpTime = specialAttackStats.SpecialBreakdown.startUpTime;
            specialAttackVars.stunTime = specialAttackStats.SpecialBreakdown.stunTime;

            // ===== ATTACK AND MOVEMENT WILL BE HANDLED WITH ANIMATIONS ===== \\
            // ===== Hit Detection will be placed on combined fire knife for attack tracking ===== \\\

            // === Temporary "Animation" of knives below should be removed later === \\


            rotateKnives = true;
        }
    }
    IEnumerator ThrownReturn()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        throwing = false;
        throwTime = 0;
        Destroy(clone);
        thrownObject.SetActive(true);
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

        breathCooldown = fireBreathCooldown;
    }

 
}
