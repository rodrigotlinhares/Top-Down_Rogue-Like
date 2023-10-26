using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeCollision : PlayerCollision
{
    private int chargeStunTime = 250, chargeForce = 1000;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (!enabled && enemy)
            base.OnCollisionEnter2D(collision);
        else if (enabled && enemy)
            Debug.Log("charge");

        //if (enemy && charging)
        //{
        //    body.velocity = Vector2.zero;
        //    Vector2 direction = ((Vector2)collision.transform.position - GetComponent<Rigidbody2D>().position).normalized;
        //    enemy.StartCoroutine(enemy.Stun(chargeStunTime));
        //    collision.rigidbody.AddForce(direction * chargeForce);
        //}
    }
}
