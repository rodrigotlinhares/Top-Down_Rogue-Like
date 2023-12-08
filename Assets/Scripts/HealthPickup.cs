using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D body, playerBody;

    private void Awake()
    {
        enabled = false;
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventSystem.events.OnPlayerDeath += Disable;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerDeath -= Disable;
    }

    private void Update()
    {
        if (enabled)
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }

    private void Disable()
    {
        enabled = false;
        body.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupRange"))
            enabled = true;
        else
            Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
