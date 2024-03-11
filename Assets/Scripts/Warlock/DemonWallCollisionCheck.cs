using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWallCollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(transform.parent.gameObject);
    }
}
