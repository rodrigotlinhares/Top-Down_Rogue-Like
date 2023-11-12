using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    protected float movementSpeed;
    protected Vector2 velocity;
    protected Rigidbody2D body;

    public abstract void Move();

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Disable(int duration)
    {
        enabled = false;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
            yield return null;
        enabled = true;
    }
}
