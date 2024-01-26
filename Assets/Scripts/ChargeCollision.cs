using System;
using UnityEngine;

public class ChargeCollision : PlayerCollision
{
    [NonSerialized] public bool charging = false;
    private Rigidbody2D body;

    private new void Awake()
    {
        base.Awake();
        body = GetComponent<Rigidbody2D>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (charging && collision.gameObject.GetComponent<Enemy>())
            body.velocity = Vector3.zero;
        else
            base.OnCollisionEnter2D(collision);
    }
}
