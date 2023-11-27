using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyZoningMovement : EnemyMovement
{
    private float zoningDistance = 5f;

    private void Update()
    {
        if (enabled && Vector2.Distance(body.position, playerBody.position) < zoningDistance)
            body.velocity = (body.position - playerBody.position).normalized * movementSpeed;
        else
            body.velocity = Vector2.zero;
    }
}
