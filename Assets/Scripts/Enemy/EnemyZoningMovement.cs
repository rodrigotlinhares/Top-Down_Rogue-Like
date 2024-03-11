using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyZoningMovement : EnemyMovement
{
    private Animator animator;
    private SpriteRenderer sprite;
    private float zoningDistance = 5f;

    new private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (enabled && Vector2.Distance(body.position, playerBody.position) < zoningDistance)
        {
            body.velocity = (body.position - playerBody.position).normalized * movementSpeed;
            animator.SetBool("moving", true);
            sprite.flipX = Mathf.Sign(body.velocity.x) == -1;
        }
        else
        {
            body.velocity = Vector2.zero;
            animator.SetBool("moving", false);
        }
    }
}
