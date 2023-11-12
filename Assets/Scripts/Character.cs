using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Health health;
    protected CCollision collision;
    protected Movement movement;
    protected Rigidbody2D body;
    protected bool mainAttackOnCooldown = false, secAttackOnCooldown = false, utilityOnCooldown = false;
    [SerializeField] protected float mainAttackCooldown, secAttackCooldown, utilityCooldown;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    protected IEnumerator Cooldown(Action<bool> flag, float time)
    {
        flag(true);
        yield return new WaitForSeconds(time);
        flag(false);
    }
}
