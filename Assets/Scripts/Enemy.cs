using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    private EnemyMovement movement;
    private Rigidbody2D body;
    [NonSerialized] public bool explosive = false;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventSystem.events.OnWarlockExplodeDots += Explode;
        EventSystem.events.OnPlayerDeath += DisableMovementForever;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnWarlockExplodeDots -= Explode;
        EventSystem.events.OnPlayerDeath -= DisableMovementForever;
    }

    public void Explode()
    {
        if(explosive)
            Instantiate(explosion, transform);
    }

    private void DisableMovementForever()
    {
        movement.enabled = false;
        body.velocity = Vector3.zero;
    }
}
