using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StationaryEnemyCollision : MonoBehaviour
{
    private EnemyHealth health;
    private Enemy enemy;
    new private DamageAnimation animation;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        animation = GetComponent<DamageAnimation>();
        enemy = GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
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
        if (trigger.gameObject.CompareTag("Projectile"))
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
