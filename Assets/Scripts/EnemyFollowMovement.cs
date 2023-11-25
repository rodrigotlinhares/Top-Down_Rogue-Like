using System;
using System.Collections;
using UnityEngine;

public class EnemyFollowMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D body, playerBody;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (enabled)
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
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