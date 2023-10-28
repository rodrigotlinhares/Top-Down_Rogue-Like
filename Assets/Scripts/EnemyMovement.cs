using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private Rigidbody2D body, playerBody;

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
