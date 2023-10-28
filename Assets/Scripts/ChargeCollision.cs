using UnityEngine;

public class ChargeCollision : PlayerCollision
{
    private int chargeStunTime = 250;
    private Rigidbody2D body;

    protected override void Awake()
    {
        base.Awake();
        duration = 250;
        body = GetComponent<Rigidbody2D>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (!enabled && enemy)
            base.OnCollisionEnter2D(collision);
        else if (enabled && enemy)
        {
            body.velocity = Vector3.zero;
            StartCoroutine(enemy.DisableMovement(chargeStunTime));
            enemy.stun.Activate(body.position);
        }
    }
}
