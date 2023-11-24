using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    private Vector2 velocity;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            velocity.x = -movementSpeed;
        if (Input.GetKey(KeyCode.D))
            velocity.x = movementSpeed;
        if (Input.GetKey(KeyCode.W))
            velocity.y = movementSpeed;
        if (Input.GetKey(KeyCode.S))
            velocity.y = -movementSpeed;
        body.velocity = velocity;
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