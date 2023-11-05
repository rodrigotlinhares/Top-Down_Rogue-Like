using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    private EnemyMovement movement;
    private Rigidbody2D body;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        body = GetComponent<Rigidbody2D>();
        GetComponent<Explosion>().enabled = false;
    }

    private void Start()
    {
        EventSystem.events.OnWarlockExplodeDots += Explode;
        EventSystem.events.OnPlayerDeath += DisableMovementForever;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnWarlockExplodeDots -= Explode;
        EventSystem.events.OnPlayerDeath -= DisableMovementForever;
    }

    private void Explode()
    {
        if (GetComponent<Explosion>().enabled)
            Instantiate(explosion, transform);
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}
