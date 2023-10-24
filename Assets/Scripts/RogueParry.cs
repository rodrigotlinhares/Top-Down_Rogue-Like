using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueParry : MonoBehaviour
{
    private int parryForce = 500;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Vector2 direction = ((Vector2)collision.transform.position - GetComponent<Rigidbody2D>().position).normalized;
            collision.rigidbody.AddForce(direction * parryForce);
        }
    }
}
