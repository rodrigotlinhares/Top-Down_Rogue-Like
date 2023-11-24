using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] protected int duration;
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
            StartCoroutine(movement.Disable(duration));
        }
        else if (collision.gameObject.GetComponent<EnemyProjectile>())
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