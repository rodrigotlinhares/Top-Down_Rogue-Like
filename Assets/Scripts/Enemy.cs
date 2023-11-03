using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovement movement;
    private Rigidbody2D body;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventSystem.events.OnPlayerDeath += DisableMovementForever;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDeath -= DisableMovementForever;
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}
