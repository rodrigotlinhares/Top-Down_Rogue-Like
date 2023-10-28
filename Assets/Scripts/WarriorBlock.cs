using UnityEngine;

public class WarriorBlock : MonoBehaviour
{
    private int stunTime = 250, stunForce = 750;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Vector2 direction = ((Vector2)collision.transform.position - GetComponent<Rigidbody2D>().position).normalized;
            StartCoroutine(enemy.DisableMovement(stunTime));
            collision.rigidbody.AddForce(direction * stunForce);
        }
    }
}
