using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] protected int duration;
    private EnemyHealth health;
    private EnemyFollowMovement movement;
    private Stun stun;
    new private DamageAnimation animation;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyFollowMovement>();
        animation = GetComponent<DamageAnimation>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Warrior warrior = collision.gameObject.GetComponent<Warrior>();
        WarriorBlock block = collision.gameObject.GetComponent<WarriorBlock>();
        RogueParry parry = collision.gameObject.GetComponent<RogueParry>();
        if ((warrior && warrior.GetComponent<ChargeCollision>().enabled) || block || parry)
        {
            StartCoroutine(movement.Disable(duration));
            stun.Activate(collision.gameObject.transform.position);
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            health.Lower(10);
            StartCoroutine(GetComponent<Enemy>().IFrames());
            StartCoroutine(animation.ChangeColor());
        }
        else if (collision.gameObject.CompareTag("WarlockProjectile"))
        {
            GetComponent<Enemy>().explosive = true;
            StartCoroutine(health.LowerOverTime(10));
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.GetComponent<WarlockKnockback>())
        {
            StartCoroutine(movement.Disable(duration));
            stun.Activate(trigger.gameObject.transform.position);
        }
        else if (trigger.gameObject.CompareTag("Projectile"))
        {
            health.Lower(10);
            StartCoroutine(GetComponent<Enemy>().IFrames());
            StartCoroutine(animation.ChangeColor());
        }
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Projectile"))
        {
            health.Lower(10);
            StartCoroutine(GetComponent<Enemy>().IFrames());
            StartCoroutine(animation.ChangeColor());
        }
    }
}
