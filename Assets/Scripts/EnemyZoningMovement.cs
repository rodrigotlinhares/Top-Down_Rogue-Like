using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyZoningMovement : EnemyMovement
{
    private Animator animator;
    private float zoningDistance = 5f;

    new private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enabled && Vector2.Distance(body.position, playerBody.position) < zoningDistance)
        {
            body.velocity = (body.position - playerBody.position).normalized * movementSpeed;
            animator.SetBool("moving", true);
        }
        else
        {
            body.velocity = Vector2.zero;
            animator.SetBool("moving", false);
        }
    }
}
