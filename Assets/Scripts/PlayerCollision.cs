using UnityEngine;

public class PlayerCollision : CCollision
{
    private PlayerHealth health;

    protected new void Awake()
    {
        base.Awake();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<Movement>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
            stun.Activate(collision.gameObject.transform.position);
            StartCoroutine(movement.Disable(duration));
        }
        else if (collision.gameObject.GetComponent<EnemyAttack>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthPickup>())
            health.Raise(10);
    }
}