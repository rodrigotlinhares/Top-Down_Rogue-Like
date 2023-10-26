using System;
using System.Collections;
using System.Collections.Generic;
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
