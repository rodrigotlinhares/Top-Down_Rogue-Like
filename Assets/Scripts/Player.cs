using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile projectile;
    public delegate void PlayerDeath();
    public static event PlayerDeath OnDeath;

    private Rigidbody2D body;
    private Vector2 velocity;
    private bool inputEnabled = true;
    private int health = 5;
    private int dashMultiplier = 4;
    private int dashTime = 250;
    private int iFrames = 250;
    private int shootForce = 1000;
    private int knockbackForce = 500;
    private float movementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
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
            if (Input.GetKey(KeyCode.Space))
                StartCoroutine(Dash());
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot(Input.mousePosition);
        }
    }

    private void Shoot(Vector3 click)
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(click) - body.position;
        direction.Normalize();
        Projectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction*shootForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            StartCoroutine(TakeDamage(collision.gameObject));
            if (health < 1)
            {
                OnDeath();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Dash()
    {
        inputEnabled = false;
        DateTime start = DateTime.Now;
        velocity.x *= dashMultiplier;
        velocity.y *= dashMultiplier;
        body.velocity = velocity;
        while ((DateTime.Now - start).Milliseconds < dashTime)
            yield return null;
        inputEnabled = true;
    }

    IEnumerator TakeDamage(GameObject other)
    {
        inputEnabled = false;
        health--;
        Vector2 direction = (body.position - other.GetComponent<Rigidbody2D>().position).normalized;
        body.AddForce(direction * knockbackForce);
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).Milliseconds < iFrames)
            yield return null;
        inputEnabled = true;
    }
}