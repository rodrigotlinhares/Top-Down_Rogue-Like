using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeath : MonoBehaviour
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
        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Animator>().SetBool("dead", true);
    }
}
