using UnityEngine;

public class ArcaneShield : MonoBehaviour
{
    public float cooldown = 5f;

    private void Update()
    {
        transform.position = transform.parent.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            Destroy(gameObject);
    }
}
