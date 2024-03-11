using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArcaneBlast : PlayerAttack
{
    [SerializeField] public float manaCost;
    [SerializeField] public float chargeTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
