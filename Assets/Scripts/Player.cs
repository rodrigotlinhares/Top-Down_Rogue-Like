using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Health health;
    protected PlayerCollision collision;
    protected PlayerMovement movement;
    protected Rigidbody2D body;
    protected bool mainAttackOnCooldown = false, secAttackOnCooldown = false, utilityOnCooldown = false;
    [SerializeField] protected float mainAttackCooldown, secAttackCooldown, utilityCooldown;

    protected void Awake()
    {
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        collision = GetComponent<PlayerCollision>();
    }

    protected IEnumerator Cooldown(Action<bool> flag, float time)
    {
        flag(true);
        yield return new WaitForSeconds(time);
        flag(false);
    }
}
