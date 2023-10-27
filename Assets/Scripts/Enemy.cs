using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health currentHealth;
    private EnemyMovement movement;
    private Rigidbody2D body;

    public Stun stun;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<Health>();
        movement = GetComponent<EnemyMovement>();
        stun = GetComponent<Stun>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        FindObjectOfType<PlayerController>().GetComponent<Health>().Death += DisableMovementForever;
    }

    void OnDisable()
    {
        FindObjectOfType<PlayerController>().GetComponent<Health>().Death -= DisableMovementForever; // TODO this is causing a null pointer when the game ends
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MageAttack>())
            currentHealth.TakeDamage();
    }

    public IEnumerator DisableMovement(int duration)
    {
        movement.enabled = false;
        body.velocity = Vector2.zero;
        DateTime start = DateTime.Now;
        while ((DateTime.Now - start).TotalMilliseconds < duration)
            yield return null;
        movement.enabled = true;
    }
}
