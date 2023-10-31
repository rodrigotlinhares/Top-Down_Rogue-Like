using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Health currentHealth;
    protected PlayerCollision collision;
    protected PlayerMovement movement;
    protected Rigidbody2D body;
    protected private int iFrames = 250, knockbackForce = 500;

    protected void Awake()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        collision = GetComponent<PlayerCollision>();
    }
}
