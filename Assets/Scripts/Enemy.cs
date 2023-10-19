using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;
    private Rigidbody2D playerBody;
    private int health = 20;
    private float movementSpeed = 3f;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        PlayerController.OnDeath += DisableMovement;
    }

    void OnDisable()
    {
        PlayerController.OnDeath -= DisableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }

    private void DisableMovement()
    {
        moving = false;
        body.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
            health--;
        if (health < 1)
            Destroy(gameObject);
    }
}
