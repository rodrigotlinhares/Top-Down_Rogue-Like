using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float cooldown;
}
