using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private PlayerController player;

    private Rigidbody2D body, playerBody;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        body.velocity = (playerBody.position - body.position).normalized * movementSpeed;
    }
}
