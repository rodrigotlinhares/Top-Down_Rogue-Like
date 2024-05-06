using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealth : EnemyHealth
{
    protected override void Death()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<EnemyDeath>().Trigger();
        }
    }
}
