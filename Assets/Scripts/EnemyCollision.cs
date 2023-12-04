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
        Parry parry = collision.gameObject.GetComponent<Parry>();
        if ((warrior && warrior.GetComponent<ChargeCollision>().enabled) || block || parry)
            stun.Activate(collision.gameObject.transform.position);
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            health.Lower(collision.gameObject.GetComponent<PlayerAttack>().damage);
            StartCoroutine(animation.ChangeColor());
        }
        else if (collision.gameObject.CompareTag("WarlockProjectile"))
        {
            enemy.explosive = true;
            StartCoroutine(health.LowerOverTime(collision.gameObject.GetComponent<Corruption>().damagePerSecond));
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.GetComponent<Demon>())
            stun.Activate(trigger.gameObject.transform.position);
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Projectile"))
        {
            health.Lower(trigger.gameObject.GetComponent<PlayerAttack>().damage);
            StartCoroutine(animation.ChangeColor());
        }
    }
}
