using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerMovement movement;
    private Stun stun;

    protected void Awake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        stun = GetComponent<Stun>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
            stun.Activate(collision.gameObject.transform.position);
            StartCoroutine(movement.Disable());
        }
        else if (collision.gameObject.GetComponent<EnemyProjectile>())
        {
            health.Lower(10);
            EventSystem.events.PlayerDamageTaken(10);
        }
        else if (collision.gameObject.GetComponent<HealthPickup>())
            health.Raise(10);
    }
}