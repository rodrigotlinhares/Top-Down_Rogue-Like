using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyProjectile projectile;
    [SerializeField] private int projectileForce;
    private Rigidbody2D body, playerBody;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        InvokeRepeating("Attack", 0f, 3f);
    }

    protected void Attack()
    {
        Vector2 direction = (playerBody.position - body.position).normalized;
        EnemyProjectile clone = Instantiate(projectile, body.transform.position, body.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
    }

    private void Start()
    {
        EventSystem.events.OnPlayerDeath += CancelInvoke;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDeath -= CancelInvoke;
    }
}
