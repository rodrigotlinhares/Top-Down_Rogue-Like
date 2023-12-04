using UnityEngine;

public class ArcaneBolt : PlayerAttack
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
