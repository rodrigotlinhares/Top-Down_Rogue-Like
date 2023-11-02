using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private int duration;
    private Health health;
    private Stun stun;
    private Movement movement;
    new private DamageAnimation animation;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        stun = GetComponent<Stun>();
        movement = GetComponent<Movement>();
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
        else if (collision.gameObject.tag == "Projectile")
        {
            health.TakeDamage?.Invoke(1);
            StartCoroutine(animation.ChangeColor());
        }
    }
}
