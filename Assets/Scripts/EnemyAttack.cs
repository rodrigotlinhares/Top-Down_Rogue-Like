using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyProjectile projectile;
    [SerializeField] private int projectileForce;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D body, playerBody;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        InvokeRepeating("PrepareAttack", 0f, 3f);
    }

    protected void PrepareAttack()
    {
        Vector2 direction = (playerBody.position - body.position).normalized;
        float angle = Vector3.Angle(direction, Vector3.up);
        sprite.flipX = Mathf.Sign(direction.x) == -1;
        animator.SetBool("shooting", true);
        animator.SetFloat("angle", angle);
    }

    protected void Attack()
    {
        Debug.Log("test");
        Vector2 direction = (playerBody.position - body.position).normalized;
        EnemyProjectile clone = Instantiate(projectile, body.transform.position, body.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
        animator.SetBool("shooting", false);
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
