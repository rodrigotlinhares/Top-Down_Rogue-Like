using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOrb : PlayerAttack
{
    [SerializeField] public float healthCost;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
