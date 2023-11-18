using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    protected float currentHealth;

    protected void Awake()
    {
        currentHealth = maxHealth;
    }

    public abstract void Lower(float amount);
    public abstract void Raise(float amount);
}
