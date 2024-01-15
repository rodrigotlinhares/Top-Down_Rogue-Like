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
        if (enabled && collision.gameObject.GetComponent<Enemy>())
            body.velocity = Vector3.zero;
        else
            base.OnCollisionEnter2D(collision);
        
    }
}
