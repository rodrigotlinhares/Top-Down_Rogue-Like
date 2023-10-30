using System;
using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    void OnEnable()
    {
        FindObjectOfType<Character>().GetComponent<Health>().Death += DisableMovementForever;
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
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
