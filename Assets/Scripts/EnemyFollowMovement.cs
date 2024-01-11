using System;
using System.Collections;
using UnityEngine;

public class EnemyFollowMovement : EnemyMovement
{
    private SpriteRenderer sprite;

    new private void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (enabled)
        {
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
            sprite.flipX = body.velocity.x < 0;
        }
    }
}