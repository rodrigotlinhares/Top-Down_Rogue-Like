using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class StationaryEnemyCollision : MonoBehaviour
{
    private EnemyHealth health;
    private Enemy enemy;
    private DamageAnimation dAnimation;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        dAnimation = GetComponent<DamageAnimation>();
        enemy = GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Warrior warrior = collision.gameObject.GetComponent<Warrior>();
        if (warrior && warrior.thorns > 0f)
        {
            health.Lower(warrior.thorns);
            StartCoroutine(dAnimation.ChangeColor());
        }
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
        if (trigger.gameObject.CompareTag("Projectile"))
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
