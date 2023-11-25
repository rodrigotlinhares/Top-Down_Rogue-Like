using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyZoningMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D body, playerBody;
    private float zoningDistance = 5;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (enabled && Vector2.Distance(body.position, playerBody.position) < zoningDistance)
            body.velocity = (body.position - playerBody.position).normalized * movementSpeed;
        else
            body.velocity = Vector2.zero;
    }

    private void Start()
    {
        EventSystem.events.OnPlayerDeath += Disable;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDeath -= Disable;
    }

    private void Disable()
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
