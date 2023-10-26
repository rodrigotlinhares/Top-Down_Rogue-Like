using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health currentHealth;
    private EnemyMovement movement;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<Health>();
        movement = GetComponent<EnemyMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        FindObjectOfType<PlayerController>().GetComponent<Health>().Death += Stop;
    }

    void OnDisable()
    {
        FindObjectOfType<PlayerController>().GetComponent<Health>().Death -= Stop;
    }

    private void Stop()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MageAttack>())
            currentHealth.TakeDamage();
    }

    public IEnumerator Stun(int time)
    {
        body.velocity = Vector2.zero;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < time)
            yield return null;
    }
}
