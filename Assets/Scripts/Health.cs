using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    private float currentHealth;
    public Action Die;
    public Action<float> TakeDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        TakeDamage += Lower;
    }

    private void OnDisable()
    {
        TakeDamage -= Lower;
    }

    private void Lower(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die?.Invoke();
            Destroy(gameObject);
        }
    }
}
