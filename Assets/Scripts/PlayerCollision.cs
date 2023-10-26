using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    protected Health playerHealth;

    protected void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // cancel abilities when this happens
        if (collision.gameObject.GetComponent<Enemy>())
            playerHealth.TakeDamage();
    }
}
