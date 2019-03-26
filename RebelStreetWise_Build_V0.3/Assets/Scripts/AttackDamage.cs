using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this script on a HIT BOX

public class AttackDamage : MonoBehaviour
{
    public Attack attack;
    public PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = attack.gameObject.GetComponent<PlayerInfo>();
    }

    [SerializeField] private int damage = 10;

    public int Damage { get { return damage; } set { damage = value; } }
}
