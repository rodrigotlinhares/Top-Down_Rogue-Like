using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerMovement movement;
    private Knockback knockback;

    protected void Awake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        knockback = GetComponent<Knockback>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
            knockback.Activate(collision.gameObject.transform.position);
            StartCoroutine(movement.Pause(knockback.duration));
        }
        else if (collision.gameObject.GetComponent<EnemyArrow>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
        }
        else if (collision.gameObject.GetComponent<HealthPickup>())
            health.Raise(10);
    }
}