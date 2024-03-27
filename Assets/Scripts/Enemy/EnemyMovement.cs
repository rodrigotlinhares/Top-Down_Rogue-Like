using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected int disableDuration = 250;
    protected Rigidbody2D body, playerBody;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        EventSystem.events.OnPlayerDeath += Disable;
    }

    protected void OnDestroy()
    {
        EventSystem.events.OnPlayerDeath -= Disable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Warrior warrior = collision.gameObject.GetComponent<Warrior>();
        Block block = collision.gameObject.GetComponent<Block>();
        Parry parry = collision.gameObject.GetComponent<Parry>();
        if ((warrior && warrior.GetComponent<ChargeCollision>().enabled) || block || parry)
            StartCoroutine(Disable(disableDuration));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Demon>())
            StartCoroutine(Disable(disableDuration));
    }

    public void Disable()
    {
        enabled = false;
        body.velocity = Vector2.zero;
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
