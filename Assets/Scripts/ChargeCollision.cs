using UnityEngine;

public class ChargeCollision : PlayerCollision
{
    private Rigidbody2D body;

    private new void Awake()
    {
        base.Awake();
        body = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (!enabled && enemy)
            base.OnCollisionEnter2D(collision);
        else if (enabled && enemy)
            body.velocity = Vector3.zero;
    }
}
