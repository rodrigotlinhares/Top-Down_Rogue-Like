using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeath : EnemyDeath
{
    public override void Trigger()
    {
        GetComponent<BoxCollider2D>().excludeLayers = Physics.AllLayers;
        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Animator>().SetBool("dead", true);
    }
}
