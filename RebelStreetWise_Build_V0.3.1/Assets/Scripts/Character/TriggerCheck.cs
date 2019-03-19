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
        //gameObject.tag = hitDetection.attacker;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.tag = hitDetection.attacker;
    }
    private void OnColllisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
          //still not working
            {
                if (col.gameObject.GetComponent<BaseMovement>())
                {
                    Debug.Log("test");
                    opponentMove = col.gameObject.GetComponent<BaseMovement>();
                    opponent = col.gameObject.GetComponent<FighterClass>();
                }
            }



        }
    }
}
