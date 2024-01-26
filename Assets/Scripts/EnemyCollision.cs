using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyHealth health;
    private Stun stun;
    private Enemy enemy;
    new private DamageAnimation animation;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        animation = GetComponent<DamageAnimation>();
        stun = GetComponent<Stun>();
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
                StartCoroutine(animation.ChangeColor());
            }
            if (warrior.GetComponent<ChargeCollision>().charging)
                stun.Activate(collision.gameObject.transform.position);
        }
        else if (block)
        {
            warrior = collision.gameObject.GetComponentInParent<Warrior>();
            if (warrior.thorns > 0f)
            {
                health.Lower(warrior.thorns);
                StartCoroutine(animation.ChangeColor());
            }
            stun.Activate(collision.gameObject.transform.position);
        }
        else if (collision.gameObject.GetComponent<Parry>())
            stun.Activate(collision.gameObject.transform.position);
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            health.Lower(collision.gameObject.GetComponent<PlayerAttack>().damage);
            StartCoroutine(animation.ChangeColor());
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
            stun.Activate(trigger.gameObject.transform.position);
        else if (trigger.gameObject.CompareTag("Projectile"))
            health.Lower(trigger.gameObject.GetComponent<PlayerAttack>().damage);
        else if (trigger.gameObject.CompareTag("LifeDrain"))
            health.LeechOverTime(trigger.gameObject.GetComponent<PlayerAttack>().damage);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("LifeDrain"))
            health.StopLeeching();
    }
}
