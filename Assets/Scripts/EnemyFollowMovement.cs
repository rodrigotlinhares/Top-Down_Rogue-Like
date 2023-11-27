using System;
using System.Collections;
using UnityEngine;

public class EnemyFollowMovement : EnemyMovement
{
    private void Update()
    {
        if (enabled)
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }
}