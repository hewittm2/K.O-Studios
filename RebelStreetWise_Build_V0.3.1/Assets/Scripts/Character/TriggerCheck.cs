using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public FighterClass opponent;
    public BaseMovement opponentMove;
    public FighterClass player;
    public HitDetection hitDetection;
    // Use this for initialization
    void Start()
    {
        hitDetection = GetComponentInParent<HitDetection>();
        player = GetComponentInParent<FighterClass>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            opponent = other.gameObject.GetComponent<FighterClass>();
            opponentMove = other.gameObject.GetComponent<BaseMovement>();
            
           
        }
    }
}
