using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile projectile;

    private Rigidbody2D body;
    private Vector2 velocity;
    private bool dashing = false;
    private int dashMultiplier = 4;
    private int dashTime = 250;
    private int shootForce = 1000;
    private float movementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
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
            if (Input.GetKey(KeyCode.Space))
                StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(Input.mousePosition);
    }

    private void Shoot(Vector3 click)
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(click) - body.position;
        direction.Normalize();
        Projectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction*shootForce);
    }

    IEnumerator Dash()
    {
        dashing = true;
        DateTime start = DateTime.Now;
        velocity.x *= dashMultiplier;
        velocity.y *= dashMultiplier;
        body.velocity = velocity;
        while ((DateTime.Now - start).Milliseconds < dashTime)
            yield return null;
        dashing = false;
    }
}