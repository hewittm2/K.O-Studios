using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    FighterClass myCC;
    private FighterClass[] allPlayers;
    private List<FighterClass> teamMate = new List<FighterClass>();
    private List<FighterClass> enemyMate = new List<FighterClass>();

    [Header("My Colliders")]
    private List<Collider> myColliders = new List<Collider>();
    [Header("Teammate Collider")]
    public List<Collider> teamMateCC = new List<Collider>();
    public float tempDist;
    public float closeDist;
    [Header("Enemy Colliders")]
    public List<Collider> enemy1CC = new List<Collider>();
    public List<Collider> enemy2CC = new List<Collider>();

    [Header("Visibilty")]
    public SkinnedMeshRenderer myCoatColor;
    public Material[] playerColors;

    void Start ()
    {
        myCC = GetComponent<FighterClass>();
        allPlayers = FindObjectsOfType<FighterClass>();

        foreach (FighterClass cc in allPlayers)
        {
            if (myCC.teamNumber == cc.teamNumber && myCC.playerNumber != cc.playerNumber)
            {
                teamMate.Add(cc);
            }
            if (myCC.teamNumber != cc.teamNumber)
                enemyMate.Add(cc);
        }

        teamMateCC.Add(teamMate[0].gameObject.GetComponent<BoxCollider>());
        teamMateCC.Add(teamMate[0].gameObject.GetComponent<CapsuleCollider>());
        teamMateCC.Add(teamMate[0].gameObject.GetComponent<CharacterController>());


        enemy1CC.Add(enemyMate[0].gameObject.GetComponent<BoxCollider>());
        enemy1CC.Add(enemyMate[0].gameObject.GetComponent<CapsuleCollider>());
        enemy1CC.Add(enemyMate[0].gameObject.GetComponent<CharacterController>());

        enemy2CC.Add(enemyMate[1].gameObject.GetComponent<BoxCollider>());
        enemy2CC.Add(enemyMate[1].gameObject.GetComponent<CapsuleCollider>());
        enemy2CC.Add(enemyMate[1].gameObject.GetComponent<CharacterController>());

        myColliders.Add(GetComponent<BoxCollider>());
        myColliders.Add(GetComponent<CapsuleCollider>());
        myColliders.Add(GetComponent<CharacterController>());


        IgnoreTheCollision(true, false);

        if (gameObject.tag == "Player1")
            myCoatColor.material = playerColors[0];
        if (gameObject.tag == "Player2")
            myCoatColor.material = playerColors[1];
        if (gameObject.tag == "Player3")
            myCoatColor.material = playerColors[2];
        if (gameObject.tag == "Player4")
            myCoatColor.material = playerColors[3];

    }

    public void IgnoreTheCollision(bool _ignoreTeammate, bool _ignoreEnemies)
    {
        if (_ignoreTeammate == true)
        {
            Physics.IgnoreCollision(myColliders[0], teamMateCC[0], true);
            Physics.IgnoreCollision(myColliders[0], teamMateCC[1], true);
            Physics.IgnoreCollision(myColliders[0], teamMateCC[2], true);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[0], true);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[1], true);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[2], true);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[0], true);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[1], true);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[2], true);
        }
        if (_ignoreEnemies == true)
        {
            Physics.IgnoreCollision(myColliders[0], enemy1CC[0], true);
            Physics.IgnoreCollision(myColliders[0], enemy1CC[1], true);
            Physics.IgnoreCollision(myColliders[0], enemy1CC[2], true);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[0], true);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[1], true);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[2], true);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[0], true);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[1], true);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[2], true);

            Physics.IgnoreCollision(myColliders[0], enemy2CC[0], true);
            Physics.IgnoreCollision(myColliders[0], enemy2CC[1], true);
            Physics.IgnoreCollision(myColliders[0], enemy2CC[2], true);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[0], true);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[1], true);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[2], true);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[0], true);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[1], true);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[2], true);
        }
    }
    public void ReEnableCollision(bool _ignoreTeammate, bool _ignoreEnemies)
    {
        if (_ignoreTeammate == true)
        {
            Physics.IgnoreCollision(myColliders[0], teamMateCC[0], false);
            Physics.IgnoreCollision(myColliders[0], teamMateCC[1], false);
            Physics.IgnoreCollision(myColliders[0], teamMateCC[2], false);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[0], false);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[1], false);
            Physics.IgnoreCollision(myColliders[1], teamMateCC[2], false);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[0], false);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[1], false);
            Physics.IgnoreCollision(myColliders[2], teamMateCC[2], false);
        }
        if (_ignoreEnemies == true)
        {
            Physics.IgnoreCollision(myColliders[0], enemy1CC[0], false);
            Physics.IgnoreCollision(myColliders[0], enemy1CC[1], false);
            Physics.IgnoreCollision(myColliders[0], enemy1CC[2], false);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[0], false);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[1], false);
            Physics.IgnoreCollision(myColliders[1], enemy1CC[2], false);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[0], false);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[1], false);
            Physics.IgnoreCollision(myColliders[2], enemy1CC[2], false);

            Physics.IgnoreCollision(myColliders[0], enemy2CC[0], false);
            Physics.IgnoreCollision(myColliders[0], enemy2CC[1], false);
            Physics.IgnoreCollision(myColliders[0], enemy2CC[2], false);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[0], false);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[1], false);
            Physics.IgnoreCollision(myColliders[1], enemy2CC[2], false);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[0], false);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[1], false);
            Physics.IgnoreCollision(myColliders[2], enemy2CC[2], false);
        }
    }

    private void Update()
    {
        tempDist = Vector3.Distance(transform.position, teamMate[0].transform.position);
        if (tempDist < closeDist)
        {
            Debug.Log("Triggered");
            transform.position = Vector3.MoveTowards(transform.position, teamMate[0].transform.position, (-1 * Time.deltaTime) / 2);
        }
    }

}
