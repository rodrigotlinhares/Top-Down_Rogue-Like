using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Health health;
    protected PlayerCollision collision;
    protected PlayerMovement movement;
    protected Rigidbody2D body;

    protected void Awake()
    {
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        collision = GetComponent<PlayerCollision>();
    }
}
