using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyHealth health;
    private Knockback knockback;
    private Enemy enemy;
    private DamageAnimation dAnimation;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        dAnimation = GetComponent<DamageAnimation>();
        knockback = GetComponent<Knockback>();
        enemy = GetComponent<Enemy>();
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Warrior warrior = collision.gameObject.GetComponent<Warrior>();
        Block block = collision.gameObject.GetComponent<Block>();
        if (warrior)
        {
            if (warrior.thorns > 0f)
            {
                health.Lower(warrior.thorns);
                StartCoroutine(dAnimation.ChangeColor());
            }
            if (warrior.GetComponent<ChargeCollision>().charging)
                knockback.Activate(collision.gameObject.transform.position);
        }
        else if (block)
        {
            warrior = collision.gameObject.GetComponentInParent<Warrior>();
            if (warrior.thorns > 0f)
            {
                health.Lower(warrior.thorns);
                StartCoroutine(dAnimation.ChangeColor());
            }
            knockback.Activate(collision.gameObject.transform.position);
        }
        else if (collision.gameObject.GetComponent<Parry>())
            knockback.Activate(collision.gameObject.transform.position);
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            health.Lower(collision.gameObject.GetComponent<PlayerAttack>().damage);
            StartCoroutine(dAnimation.ChangeColor());
        }
        else if (collision.gameObject.CompareTag("Corruption"))
        {
            enemy.explosive = true;
            health.LeechOverTime(collision.gameObject.GetComponent<Corruption>().damageOverTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.GetComponent<Demon>())
            knockback.Activate(trigger.gameObject.transform.position, trigger.gameObject.GetComponent<Demon>().stunForce);
        else if (trigger.gameObject.CompareTag("Projectile"))
        {
            health.Lower(trigger.gameObject.GetComponent<PlayerAttack>().damage);
            StartCoroutine(dAnimation.ChangeColor());
        }
        else if (trigger.gameObject.CompareTag("LifeDrain"))
            health.LeechOverTime(trigger.gameObject.GetComponent<PlayerAttack>().damage);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("LifeDrain"))
            health.StopLeeching();
    }
}
