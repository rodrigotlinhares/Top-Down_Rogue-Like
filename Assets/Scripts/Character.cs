using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Health currentHealth;
    protected BaseCollision collision;
    protected PlayerMovement movement;
    protected Rigidbody2D body;
    protected private int iFrames = 250, knockbackForce = 500;

    protected void Awake()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        collision = GetComponent<BaseCollision>();
    }
}
