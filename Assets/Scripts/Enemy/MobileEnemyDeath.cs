using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEnemyDeath : EnemyDeath
{
    public override void Trigger()
    {
        GetComponent<BoxCollider2D>().excludeLayers = Physics.AllLayers;
        GetComponent<EnemyMovement>().Disable();
        GetComponent<Animator>().fireEvents = false;
        GetComponent<Animator>().SetBool("dead", true);
    }
}
