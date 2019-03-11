using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ryan Van Dusen, Chris B.
// 3/10/2019

public class MakoaSpecialAttacks : SpecialAttackTemplate {

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
    private bool throwSetUp = false;

    #endregion

    #region ForwardSpecialVars

    public ParticleSystem coneOfFire;
    public float breathWaitTime = 2f;
    public float fireDamage;

    List<ParticleCollisionEvent> collisionEvents;

    #endregion

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();

        coneOfFire.Stop();
    }

    void Update()
    {
        if (throwing == false)
        {

        }

        if (throwing == true)
        {
            //rotates clone
            clone.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            //starts a counter
            throwTime = throwTime + Time.deltaTime;
            //moves clone
            clone.transform.position += (transform.forward * throwSpeed) * Time.deltaTime;

            //once the counter is >= to the time set in the inspecter the clone destroys and the orginal object is set back to active
            if (throwTime >= totalThrowTime)
            {

                throwing = false;
                throwSetUp = false;
                throwTime = 0;
                Destroy(clone);
                thrownObject.SetActive(true);

            }
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
    }
    public override void ForwardSA(SpecialAttacks forward)
    {
        StartCoroutine("WaitTime", breathWaitTime);
        coneOfFire.Play();
    }
    public override void JumpSA(SpecialAttacks jump)
    {

    }
    public override void DownSA(SpecialAttacks down)
    {

    }
    public override void BreakdownSA(SpecialAttacks breakdown)
    {

    }

    // ----- Never Triggers -----
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hello!");

        ParticlePhysicsExtensions.GetCollisionEvents(coneOfFire, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Debug.Log("FFFF");
            specialAttackVars.damage = fireDamage;
        }

    }

    IEnumerator WaitTime(float wait)
    {
        yield return new WaitForSeconds(wait);
    }
}
