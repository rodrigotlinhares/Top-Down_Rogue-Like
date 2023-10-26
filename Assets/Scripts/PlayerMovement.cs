using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private Vector2 velocity;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Move()
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
}
