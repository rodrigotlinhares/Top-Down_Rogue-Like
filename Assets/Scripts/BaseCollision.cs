using UnityEngine;

public class BaseCollision : MonoBehaviour
{
    [SerializeField]
    protected int duration;
    protected Health playerHealth;
    protected Stun stun;
    protected Movement movement;

    protected virtual void Awake()
    {
        playerHealth = GetComponent<Health>();
        stun = GetComponent<Stun>();
        movement = GetComponent<Movement>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            playerHealth.TakeDamage();
            stun.Activate(collision.gameObject.transform.position);
            StartCoroutine(movement.Disable(duration));
        }
    }
}