using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolloCoupDG : MonoBehaviour
{
    public GameObject theTrail;
    public GameObject coupExplosion;
    [Header("Control Coup Stars Speed")][Space(0.5f)]
    [Tooltip("How fast the star moves (Start Speed)")][Range(3,10)]
    public float moveSpeed = 5f;
    [Tooltip("This value increases the rate at which it picks up speed ")][Range(10,25)]
    public int speedIncreaseOT = 10; 
    [Header("Read Only - Value ignored.")][Space(0.5f)]
    [SerializeField] private FighterClass[] fightersC;
    [SerializeField] private GameObject[] enemies;

    [HideInInspector]
    public FighterClass selfCheck;
    [HideInInspector]
    public bool moveTowards;


    private void Awake()
    {
        enemies = new GameObject[2];
        fightersC = FindObjectsOfType<FighterClass>();
        int holder = 0;
        foreach (FighterClass fighter in fightersC)
        {
            if (fighter.teamNumber != selfCheck.teamNumber)
            {
                enemies[holder] = fighter.gameObject;
                holder++;
            }
        }
    }	
	void Update ()
    {
        if (moveTowards == true)
        {
            if (theTrail.activeInHierarchy == false)
            {
                theTrail.SetActive(true);
            }
            moveSpeed += Time.deltaTime * speedIncreaseOT;
            transform.position = Vector3.MoveTowards(transform.position, enemies[0].transform.position, 0.05f * moveSpeed);
            float dist = Vector3.Distance(transform.position, enemies[0].transform.position);
           
            if (dist <= 0.06f)
            {
                theTrail.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
                moveTowards = false;
                Rigidbody movement = enemies[0].transform.root.gameObject.GetComponent<Rigidbody>();
                movement.constraints = RigidbodyConstraints.None;
                movement.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                movement.velocity = new Vector3(0, 0, 0);
                movement.angularVelocity = new Vector3(0, 0, 0);
                movement.velocity += (new Vector3(10, 10, 0) * 10);
                StartCoroutine(ExplosionWait());
                
            }
        }
	}

    IEnumerator ExplosionWait()
    {
        coupExplosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
