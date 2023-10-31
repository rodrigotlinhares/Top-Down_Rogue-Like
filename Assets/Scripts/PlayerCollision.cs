using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    protected int duration;
    protected Health health;
    protected Stun stun;
    protected Movement movement;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        stun = GetComponent<Stun>();
        movement = GetComponent<Movement>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            health.TakeDamage();
            stun.Activate(collision.gameObject.transform.position);
            StartCoroutine(movement.Disable(duration));
        }
    }
}