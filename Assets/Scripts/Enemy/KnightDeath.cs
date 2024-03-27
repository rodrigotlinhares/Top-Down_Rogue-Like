using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeath : MonoBehaviour
{
    private void Start()
    {
        EventSystem.events.OnEnemyDeath += Die;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnEnemyDeath -= Die;
    }

    private void Die()
    {
        GetComponent<BoxCollider2D>().excludeLayers = Physics.AllLayers;
        GetComponent<EnemyMovement>().Disable();
        GetComponent<Animator>().SetBool("dead", true);
    }
}
