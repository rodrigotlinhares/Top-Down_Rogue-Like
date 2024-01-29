using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectedArrow : PlayerAttack
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
