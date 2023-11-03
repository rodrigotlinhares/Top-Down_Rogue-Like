using UnityEngine;

public class MageShield : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
            Destroy(gameObject);
    }
}
