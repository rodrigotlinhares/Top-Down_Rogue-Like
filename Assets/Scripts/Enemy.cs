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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().Death += DisableMovementForever;
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}
