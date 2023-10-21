using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController player;

    private Rigidbody2D body;
    private int health = 20;
    private float movementSpeed = 3f;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        PlayerController.OnDeath += Stop;
    }

    void OnDisable()
    {
        PlayerController.OnDeath -= Stop;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            body.velocity = (player.GetComponent<Rigidbody2D>().position - body.position).normalized * movementSpeed;
    }

    private void Stop()
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

    public IEnumerator Stun(int time)
    {
        moving = false;
        body.velocity = Vector2.zero;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < time)
            yield return null;
        moving = true;
    }
}
