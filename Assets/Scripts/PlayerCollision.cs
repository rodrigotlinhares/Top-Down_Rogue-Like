using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    protected int duration;
    protected Health playerHealth;
    protected Stun stun;

    protected virtual void Awake()
    {
        playerHealth = GetComponent<Health>();
        stun = GetComponent<Stun>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // cancel abilities when this happens
        if (collision.gameObject.GetComponent<Enemy>())
        {
            playerHealth.TakeDamage();
            stun.Activate(collision.gameObject.transform.position);
            StartCoroutine(Character.DisableInput(duration));
        }
    }
}