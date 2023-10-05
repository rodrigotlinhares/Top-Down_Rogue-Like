using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 velocity;
    private float movementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        velocity = body.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = 0f;
        velocity.y = 0f;
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