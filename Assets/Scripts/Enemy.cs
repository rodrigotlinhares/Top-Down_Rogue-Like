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

    void OnEnable()
    {
        GameObject.FindAnyObjectByType<Player>().GetComponent<Health>().Die += DisableMovementForever;
    }

    void OnDisable()
    {
        Player player = GameObject.FindAnyObjectByType<Player>();
        if (player)
            player.GetComponent<Health>().Die -= DisableMovementForever;
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}
