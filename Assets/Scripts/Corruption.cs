using UnityEngine;

public class Corruption : PlayerAttack
{
    [SerializeField] public float damageOverTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
