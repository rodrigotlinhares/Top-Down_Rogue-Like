using UnityEngine;

public class EnemyMovement : Movement
{
    private Rigidbody2D playerBody;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public override void Move()
    {
        if(enabled)
            body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }
}