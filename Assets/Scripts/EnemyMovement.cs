using UnityEngine;

public class EnemyMovement : Movement
{
    private Rigidbody2D playerBody;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }
}