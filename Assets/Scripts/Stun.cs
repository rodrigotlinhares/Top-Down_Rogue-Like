using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    [SerializeField]
    private int force;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector2 origin)
    {
        Vector2 direction = (body.position - origin).normalized;
        body.AddForce(direction * force);
    }
}
