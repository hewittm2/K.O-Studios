using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ryan Van Dusen
// 3/5/19

//Inhertaing from FIGHTCLASS
public class Chracter2Specials : FighterClass {


    public ParticleSystem empoweredParticle;
    public float damageBuff = 1.1f;
    public int maxEmpowerTime;
    private float currentEmpowerTime = 0;

    public GameObject thrownObject;
    private GameObject clone;
    private Vector3 spawnPosition;

    public float throwDistance;
    public float maxThrowDistance;
    public float maxThrowTime;
    private float throwTime;
    private bool throwing = false;
    private bool retracting = false;

	// Use this for initialization
	void Start () {
        damage = 5;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("B_1") && throwing == false && retracting == false)
        {
            clone = Instantiate(thrownObject);
            clone.transform.position = thrownObject.transform.position;
            thrownObject.SetActive(false);

            clone.transform.eulerAngles = new Vector3(0, 0, 0);
            throwing = true;
            
        }
        if (throwing == true)
        {
          
            throwTime = throwTime + Time.deltaTime;
            clone.transform.Translate(new Vector3(1 + (throwDistance * Time.deltaTime), 0, 0));
            if(throwTime >= maxThrowTime)
            {
                throwing = false;
                //retracting = true;
                throwTime = 0;
                thrownObject.transform.localPosition = (new Vector3(0, 0, 0));
                Destroy(clone);
                thrownObject.SetActive(true);
                   
            }
        }
        //if(retracting == true)
        //{
            
        //    throwTime = throwTime + Time.deltaTime;
        //    thrownObject.transform.Translate(new Vector3((throwDistance * -1) * Time.deltaTime, 0, 0));
        //    if (throwTime >= maxThrowTime)
        //    {
               
        //        throwTime = 0;
        //        retracting = false;

            
        //    }
        //}





    }

    void NeutralSpecial()
    {
        if (Input.GetButtonDown("B_1"))
        {
            if (currentEmpowerTime == 0)
            {
                damage = damage * damageBuff;
                currentEmpowerTime = currentEmpowerTime + Time.deltaTime;
                empoweredParticle.gameObject.SetActive(true);
            }
        }
        if (currentEmpowerTime >= maxEmpowerTime)
        {
            damage = damage / damageBuff;
            empoweredParticle.gameObject.SetActive(false);
            currentEmpowerTime = 0;
        }
    }
    void BackSpecial()
    {

    }

}
