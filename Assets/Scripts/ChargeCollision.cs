using UnityEngine;

public class ChargeCollision : BaseCollision
{
    private Rigidbody2D body;

    protected override void Awake()
    {
        base.Awake();
        duration = 250; // TODO remove
        body = GetComponent<Rigidbody2D>();
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
