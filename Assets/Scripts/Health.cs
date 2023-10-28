using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health;

    public Action Death;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            if(Death != null)
                Death();
            Destroy(gameObject);
        }
    }
}
