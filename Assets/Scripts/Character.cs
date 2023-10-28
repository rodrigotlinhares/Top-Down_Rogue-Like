using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Health currentHealth;
    protected PlayerCollision playerCollision;
    protected PlayerMovement movement;
    protected Stun stun;
    protected Rigidbody2D body;
    protected private int iFrames = 250, knockbackForce = 500;

    public static bool inputEnabled = true;

    protected void Awake()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    public static IEnumerator DisableInput(int duration)
    {
        inputEnabled = false;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
            yield return null;
        inputEnabled = true;
    }
}
