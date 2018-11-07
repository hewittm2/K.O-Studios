using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this script on a HIT BOX

[RequireComponent(typeof(BoxCollider))]
public class AttackV2 : MonoBehaviour
{
    public Attack attack;

    [SerializeField] private int damage = 10;

    public int Damage { get { return damage; } set { damage = value; } }
}
