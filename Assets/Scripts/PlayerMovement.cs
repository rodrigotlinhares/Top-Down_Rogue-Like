using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Stun stun;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        stun = GetComponent<Stun>();
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
    public IEnumerator Disable()
    {
        enabled = false;
        yield return new WaitForSeconds(stun.duration);
        enabled = true;
    }
}