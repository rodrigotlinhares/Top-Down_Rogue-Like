using System;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    private float acceleration = 0.05f;
    private Vector2 velocity;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventSystem.events.OnGamePaused += Disable;
        EventSystem.events.OnGameUnpaused += Enable;
        EventSystem.events.OnRogueMovementSpeedChosen += Increase;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnGamePaused -= Disable;
        EventSystem.events.OnGameUnpaused -= Enable;
        EventSystem.events.OnRogueMovementSpeedChosen -= Increase;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            velocity.x -= acceleration;
        else if (Input.GetKey(KeyCode.D))
            velocity.x += acceleration;
        else
            velocity.x = Math.Sign(velocity.x) * (Math.Abs(velocity.x) - acceleration);
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        if (Input.GetKey(KeyCode.S))
            velocity.y -= acceleration;
        else if (Input.GetKey(KeyCode.W))
            velocity.y += acceleration;
        else
            velocity.y = Math.Sign(velocity.y) * (Math.Abs(velocity.y) - acceleration);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);

        body.velocity = velocity;
    }

    public Vector2 CurrentInput()
    {
        Vector2 v = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A))
            v.x = -1;
        if (Input.GetKey(KeyCode.D))
            v.x = 1;
        if (Input.GetKey(KeyCode.S))
            v.y = -1;
        if (Input.GetKey(KeyCode.W))
            v.y = 1;
        return v;
    }

    private void Enable()
    {
        enabled = true;
    }

    private void Disable()
    {
        enabled = false;
    }

    public IEnumerator Pause(float duration)
    {
        enabled = false;
        yield return new WaitForSeconds(duration);
        enabled = true;
    }

    private void Increase(float amount)
    {
        maxSpeed *= 1 + amount;
    }
}