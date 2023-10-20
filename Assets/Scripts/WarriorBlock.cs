using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBlock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Vector2 direction = (GetComponent<Rigidbody2D>().position - (Vector2)collision.transform.position).normalized;
            collision.rigidbody.AddForce(direction * 1000);
        }
    }
}
